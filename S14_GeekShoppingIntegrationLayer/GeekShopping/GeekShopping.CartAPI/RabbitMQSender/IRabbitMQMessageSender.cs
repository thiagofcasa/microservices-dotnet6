using GeekShopping.MessageBus;

namespace GeekShopping.CartAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        //um void pois mandamos a msg para o rabbit e não esperamos um retorno por ser assinc.
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
