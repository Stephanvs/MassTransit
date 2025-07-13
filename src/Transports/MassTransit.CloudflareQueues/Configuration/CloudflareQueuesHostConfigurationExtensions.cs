
namespace MassTransit.Configuration
{
    using System;


    public static class CloudflareQueuesHostConfigurationExtensions
    {
        public static void Host(this ICloudflareQueuesBusFactoryConfigurator configurator, Action<ICloudflareQueuesHostConfigurator> configureHost)
        {
            var hostConfigurator = new CloudflareQueuesHostConfigurator();

            configureHost(hostConfigurator);

            configurator.Host(hostConfigurator.Settings);
        }
    }
}
