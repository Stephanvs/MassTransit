
namespace MassTransit.Configuration
{
    using System;


    public static class CloudflareQueuesBusConfigurationExtensions
    {
        public static IBusControl CreateUsingCloudflareQueues(this IBusFactorySelector selector, Action<ICloudflareQueuesBusFactoryConfigurator> configure)
        {
            return new CloudflareQueuesBusFactory().Create(configure);
        }
    }
}
