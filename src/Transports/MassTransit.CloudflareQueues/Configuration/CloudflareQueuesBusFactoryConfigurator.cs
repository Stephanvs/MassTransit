
namespace MassTransit.Configuration
{
    public class CloudflareQueuesBusFactoryConfigurator : BusFactoryConfigurator, ICloudflareQueuesBusFactoryConfigurator
    {
        public CloudflareQueuesHostSettings HostSettings { get; private set; }

        public CloudflareQueuesBusFactoryConfigurator(IBusConfiguration busConfiguration) : base(busConfiguration)
        {
        }

        public void Host(CloudflareQueuesHostSettings settings)
        {
            HostSettings = settings;
        }

        public void ReceiveEndpoint(CloudflareQueuesReceiveEndpointSettings settings, IReceiveEndpointConfigurator configurator)
        {
            AddReceiveEndpoint(settings.QueueName, configurator);
        }
    }
}
