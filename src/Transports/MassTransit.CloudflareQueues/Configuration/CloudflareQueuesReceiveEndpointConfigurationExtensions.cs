namespace MassTransit.Configuration
{
    using System;

    public static class CloudflareQueuesReceiveEndpointConfigurationExtensions
    {
        public static void ReceiveEndpoint(
            this ICloudflareQueuesBusFactoryConfigurator configurator,
            string queueName,
            Action<ICloudflareQueuesReceiveEndpointConfigurator> configureEndpoint)
        {
            var settings = new CloudflareQueuesReceiveEndpointSettings { QueueName = queueName };
            var endpointConfigurator = new CloudflareQueuesReceiveEndpointConfigurator(new EndpointConfiguration());

            configureEndpoint(endpointConfigurator);

            configurator.ReceiveEndpoint(settings, endpointConfigurator.Build());
        }
    }
}
