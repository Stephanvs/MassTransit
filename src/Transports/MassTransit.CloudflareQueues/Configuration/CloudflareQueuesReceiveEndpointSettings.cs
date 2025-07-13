
namespace MassTransit.Configuration
{
    public class CloudflareQueuesReceiveEndpointSettings
    {
        public string QueueName { get; set; }
        public int BatchSize { get; set; } = 10;
        public int VisibilityTimeout { get; set; } = 30;
    }
}
