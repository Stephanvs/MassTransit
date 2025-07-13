
namespace MassTransit.Contexts
{
    using MassTransit.Transports;


    public class CloudflareQueuesReceiveContext : BaseReceiveContext
    {
        public CloudflareQueuesReceiveContext(byte[] body, bool redelivered, IHeaderProvider headerProvider) : base(redelivered)
        {
            Body = new CloudflareQueuesMessageBody(body);
            HeaderProvider = headerProvider;
        }

        protected override IHeaderProvider HeaderProvider { get; }

        public override MessageBody Body { get; }
    }
}
