namespace NMH_WebAPI.Messaging
{
    public interface IProducer
    {
        void SendMessage<T>(T message);
    }
}
