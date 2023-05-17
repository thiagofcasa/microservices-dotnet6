using GeekShopping.CartAPI.Messages;
using GeekShopping.MessageBus;
using RabbitMQ.Client;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace GeekShopping.CartAPI.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;

        private readonly int _port;

        public RabbitMQMessageSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
            //_port = 8080;
        }

        public void SendMessage(BaseMessage message, string queueName)
        {

            if (ConnetionExists())
            {
                //Definir o chanel que usaremos
                using var channel = _connection.CreateModel();

                //Definir as propriedades
                //Definir uma fila
                channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);

                //converter a message recebida por parametro em um array de bytes.
                byte[] body = GetMessageAsByteArray(message);

                //Fazer a publicação passando os exchanges, queue name, propriedades...
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            }
        }

        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            //precisamos definir uma propriedade de serialização options, o WirteIntended diz que deverá
            //ser serializada a classe derivada também, senão seria serializada abenas a BaseMessage.
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            //Primeiro converter em Json, passnado o tipo e fazendo o casting.
            var json = JsonSerializer.Serialize<CheckoutHeaderVO>((CheckoutHeaderVO)message, options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }
        private void CreateConnection()
        {
            try
            {
                //Criar uma connection factory
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    UserName = _userName,
                    Password = _password,
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception)
            {
                throw;  //Log Exception
            }
        }

        private bool ConnetionExists()
        {
            //Se a conexao existir retornamos a conexão para não criar uma nova
            if (_connection != null) return true;

            CreateConnection();
            return _connection != null;
        }

    }
}
