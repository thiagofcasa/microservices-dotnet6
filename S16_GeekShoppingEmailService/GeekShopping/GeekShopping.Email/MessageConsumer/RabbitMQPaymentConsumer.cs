using GeekShopping.Email.Messages;
using GeekShopping.Email.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShopping.Email.MessageConsumer
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private readonly EmailRepository _repository;
        private IConnection _connection;
        private IModel _channel;
        private const string ExchangeName = "DirectPaymentUpdateExchange";

        private const string PaymentEmailUpdateQueueName = "PaymentEmailUpdateQueueName";
        

        public RabbitMQPaymentConsumer(EmailRepository repository)
        {
            _repository = repository;

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            //_channel.QueueDeclare(queue: "orderpaymentresultqueue", false, false, false, arguments: null);
            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);

            //declarar uma fila
            _channel.QueueDeclare(PaymentEmailUpdateQueueName, false, false, false, null);

            //agora que temos o nome do exchange e da queue fazemos o bind.
            _channel.QueueBind(PaymentEmailUpdateQueueName, ExchangeName, "PaymentEmail"); 
        }

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
                UpdatePaymentResultMessage message = JsonSerializer.Deserialize<UpdatePaymentResultMessage>(content);
                ProcessLogs(message).GetAwaiter().GetResult();

                //Para remover a mensagem da lista
                _channel.BasicAck(evt.DeliveryTag, false);
            };

            _channel.BasicConsume(PaymentEmailUpdateQueueName, false, consumer);

            return Task.CompletedTask;
        }


        private async Task ProcessLogs(UpdatePaymentResultMessage message)
        {
            try
            {
                await _repository.LogEmail(message);
            }
            catch (Exception)
            {
                //Log
                throw;
            }

        }
    }
}
