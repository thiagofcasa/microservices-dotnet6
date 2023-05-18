using GeekShopping.PaymentAPI.Messages;
using GeekShopping.PaymentAPI.RabbitMQSender;
using GeekShopping.PaymentProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShopping.PaymentAPI.MessageConsumer
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private IConnection _connection;
        //chanel responsavel por consumir uma fila
        private IModel _channel;
        private IRabbitMQMessageSender _rabbitMQMessageSender;
        private readonly IProcessPayment _processPayment;

        //Processar a ordem de pagamento
        public RabbitMQPaymentConsumer(IProcessPayment processPayment, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _processPayment = processPayment;
            _rabbitMQMessageSender = rabbitMQMessageSender;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            //Criar uma conexão
            _connection = factory.CreateConnection();

            //Criar um modelo
            //Definir o chanel que usaremos
            _channel = _connection.CreateModel();

            //Definir as propriedades
            //Definir uma fila
            _channel.QueueDeclare(queue: "orderpaymentprocessqueue", false, false, false, arguments: null);
        }

        //Devolver a resposta da ordem de pagamento.
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //se cancelar ele lançara uma exceção.
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            //consumir a fila passando o channel e o evento.
            consumer.Received += (chanel, evt) =>
            {
                //pegar o conteudo que vem em um array de bytes e converter para string
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());

                //Definir o checkoutVO que será igual a deserialização do conteudo acima.
                PaymentMessage paymentMessage = JsonSerializer.Deserialize<PaymentMessage>(content);

                ProcessPayment(paymentMessage).GetAwaiter().GetResult();

                //Para remover a mensagem da lista
                _channel.BasicAck(evt.DeliveryTag, false);
            };

            _channel.BasicConsume("orderpaymentprocessqueue", false, consumer);
            
            return Task.CompletedTask;
        }

        private async Task ProcessPayment(PaymentMessage vo)
        {
            //Esse mock só retornará null, aqui representa o que seria em um serviço
            //chamando um serviço de pagamento que poderia estar em outro microserviço ou job
            var result = _processPayment.PaymentProcessor();

            UpdatePaymentResultMessage paymentResult = new()
            {
                Status = result,
                OrderId = vo.OrderId,
                Email = vo.Email,
            };

            //Tentar publicar a mensagem no RabbitMQ
            try
            {
                //passar o objeto e o nome da fila
                _rabbitMQMessageSender.SendMessage(paymentResult);
            }
            catch (Exception)
            {
                //Log
                throw;
            }

        }
    }
}
