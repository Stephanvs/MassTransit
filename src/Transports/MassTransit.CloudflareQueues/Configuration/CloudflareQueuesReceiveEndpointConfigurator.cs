
namespace MassTransit.Configuration
{
    public class CloudflareQueuesReceiveEndpointConfigurator : ReceiveEndpointConfigurator, ICloudflareQueuesReceiveEndpointConfigurator
    {
        public CloudflareQueuesReceiveEndpointConfigurator(IEndpointConfiguration configuration) : base(configuration)
        {
        }
    }
}
