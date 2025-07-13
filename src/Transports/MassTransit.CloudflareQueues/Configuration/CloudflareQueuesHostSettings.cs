
namespace MassTransit.Configuration
{
    using System;


    public class CloudflareQueuesHostSettings
    {
        public string AccountId { get; set; }
        public string AccountHash { get; set; }
        public string ApiToken { get; set; }

        public Uri ServiceUri => new Uri($"https://api.cloudflare.com/client/v4/accounts/{AccountId}/queues");
    }
}
