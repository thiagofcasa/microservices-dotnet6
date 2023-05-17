using GeekShopping.MessageBus;

namespace GeekShopping.PaymentAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        //tirada a fila do parametro pois com exchange não precisa passar a fila.
        void SendMessage(BaseMessage baseMessage);
    }
}
