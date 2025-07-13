
namespace MassTransit
{
    using System;
    using System.Threading.Tasks;
    using Configuration;
    using MassTransit.Transports;

    public class CloudflareQueuesHost : IHost
    {
        private readonly CloudflareQueuesHostSettings _settings;

        public CloudflareQueuesHost(CloudflareQueuesHostSettings settings)
        {
            _settings = settings;
        }

        public HostHandle Start()
        {
            return new HostHandle(Task.CompletedTask, Stop);
        }

        private Task Stop()
        {
            return Task.CompletedTask;
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("CloudflareQueuesHost");
        }

        public Uri Address => _settings.ServiceUri;

        public IReceiveTransport CreateReceiveTransport(CloudflareQueuesReceiveEndpointSettings settings, Action<IReceiveEndpointConfigurator> configure)
        {
            var configurator = new CloudflareQueuesReceiveEndpointConfigurator(new EndpointConfiguration());
            configure(configurator);

            return new CloudflareQueuesReceiveTransport(_settings, settings);
        }

        public ISendTransport CreateSendTransport(Uri address)
        {
            return new CloudflareQueuesSendTransport(_settings);
        }
    }
}
