using GeekShopping.OrderAPI.Messages;
using GeekShopping.OrderAPI.Model;
using GeekShopping.OrderAPI.RabbitMQSender;
using GeekShopping.OrderAPI.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShopping.OrderAPI.MessageConsumer
{
    public class RabbitMQCheckoutConsumer : BackgroundService
    {
        private readonly OrderRepository _repository;
        private IConnection _connection;
        //chanel responsavel por consumir uma fila
        private IModel _channel;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public RabbitMQCheckoutConsumer(OrderRepository repository, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _repository = repository;
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
            _channel.QueueDeclare(queue: "checkoutqueue", false, false, false, arguments: null);
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

                //Definir o checkoutVO que será igual a deserialização do conteudo acima.
                CheckoutHeaderVO vo = JsonSerializer.Deserialize<CheckoutHeaderVO>(content);

                ProcessOrder(vo).GetAwaiter().GetResult();

                //Para remover a mensagem da lista
                _channel.BasicAck(evt.DeliveryTag, false);
            };

            _channel.BasicConsume("checkoutqueue", false, consumer);

            return Task.CompletedTask;
        }


        private async Task ProcessOrder(CheckoutHeaderVO vo)
        {
            //Processar o checkoutHeaderVO que chegou da fila
            //Conversão de checkouHeaderVO para OrderHeader
            OrderHeader order = new()
            {
                //fazer o binding de forma manual, setar um objeto no outro
                UserId = vo.UserId,
                FirstName = vo.FirstName,
                LastName = vo.LastName,
                //Precisa passar o new para o EF conseguir fazer a perssistencia
                //tanto do OrderHeader como do OrderDetail
                OrderDetails = new List<OrderDetail>(),
                CardNumber = vo.CardNumber,
                CouponCode = vo.CouponCode,
                CVV = vo.CVV,
                DiscountAmount = vo.DiscountAmount,
                Email = vo.Email,
                ExpireMonthYear = vo.ExpireMonthYear,
                OrderTime = DateTime.Now,
                PurchaseAmount = vo.PurchaseAmount,
                //só quando finalizar o pagamento ele será true.
                PaymentStatus = false,
                Phone = vo.Phone,
                DateTime = vo.DateTime
            };

            //iterar sobre todos os CartDetails e setar
            foreach (var details in vo.CartDetails)
            {
                OrderDetail detail = new()
                {
                    ProductId = details.ProductId,
                    ProductName = details.Product.Name,
                    Price = details.Product.Price,
                    Count = details.Count
                };
                //add o cartTotalItens e calcular a qtd
                order.CartTotalItens += details.Count;
                order.OrderDetails.Add(detail);
            }

            //Salvar no repositório
            await _repository.AddOrder(order);

            //Aqui é efetuado o pagamento

            //Montar o objeto
            PaymentVO payment = new()
            {
                Name = order.FirstName + " " + order.LastName,
                CardNumber = order.CardNumber,
                CVV =  order.CVV,
                ExpiryMonthYear = order.ExpireMonthYear,
                OrderId = order.Id,
                PurchaseAmount = order.PurchaseAmount,
                Email= order.Email
            };

            //Tentar publicar a mensagem no RabbitMQ
            try
            {
                //passar o objeto e o nome da fila
                _rabbitMQMessageSender.SendMessage(payment, "orderpaymentprocessqueue");
            }
            catch (Exception)
            {
                //Log
                throw;
            }

        }
    }
}
