
namespace MassTransit.Contexts
{
    using MassTransit.Context;


    public class CloudflareQueuesSendContext<T> : MessageSendContext<T> where T : class
    {
        public CloudflareQueuesSendContext(T message) : base(message)
        {
        }
    }
}
