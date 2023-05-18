using GeekShopping.OrderAPI.Messages;
using GeekShopping.OrderAPI.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShopping.OrderAPI.MessageConsumer
{
    public class RabbitMQPaymentConsumer : BackgroundService
    {
        private readonly OrderRepository _repository;
        private IConnection _connection;
        private IModel _channel;
        private const string ExchangeName = "DirectPaymentUpdateExchange";
        private const string PaymentOrderUpdateQueueName = "PaymentOrderUpdateQueueName";

        public RabbitMQPaymentConsumer(OrderRepository repository)
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

            _channel.QueueDeclare(PaymentOrderUpdateQueueName, false, false, false, null);
            
            //agora que temos o nome do exchange e da queue fazemos o bind.
            _channel.QueueBind(PaymentOrderUpdateQueueName, ExchangeName, "PaymentOrder"); 
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
                UpdatePaymentResultVO vo = JsonSerializer.Deserialize<UpdatePaymentResultVO>(content);
                UpdatePaymentStatus(vo).GetAwaiter().GetResult();

                //Para remover a mensagem da lista
                _channel.BasicAck(evt.DeliveryTag, false);
            };

            _channel.BasicConsume(PaymentOrderUpdateQueueName, false, consumer);

            return Task.CompletedTask;
        }


        private async Task UpdatePaymentStatus(UpdatePaymentResultVO vo)
        {
            try
            {
                //passar o objeto e o nome da fila
                await _repository.UpdateOrderPaymentStatus(vo.OrderId, vo.Status);
            }
            catch (Exception)
            {
                //Log
                throw;
            }

        }
    }
}
