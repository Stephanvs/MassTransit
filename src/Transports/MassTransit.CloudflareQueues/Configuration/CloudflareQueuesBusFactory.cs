namespace MassTransit.Configuration
{
    using System;

    public class CloudflareQueuesBusFactory
    {
        /// <summary>
        /// Configure and create a bus for Cloudflare Queues
        /// </summary>
        /// <param name="configure">The configuration callback to configure the bus</param>
        /// <returns>An instance of <see cref="IBusControl"/> configured for Cloudflare Queues.</returns>
        public IBusControl Create(Action<ICloudflareQueuesBusFactoryConfigurator> configure)
        {
            var busConfiguration = new CloudflareQueuesBusConfiguration();
            var configurator = new CloudflareQueuesBusFactoryConfigurator(busConfiguration);

            configure(configurator);

            var host = new CloudflareQueuesHost(configurator.HostSettings);

            return busConfiguration.Build(host);
        }
    }
}
