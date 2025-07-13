
namespace MassTransit.Configuration
{
    public class CloudflareQueuesHostConfigurator : ICloudflareQueuesHostConfigurator
    {
        private readonly CloudflareQueuesHostSettings _settings = new CloudflareQueuesHostSettings();

        public CloudflareQueuesHostConfigurator()
        {
        }

        public void AccountId(string accountId)
        {
            _settings.AccountId = accountId;
        }

        public void AccountHash(string accountHash)
        {
            _settings.AccountHash = accountHash;
        }

        public void ApiToken(string apiToken)
        {
            _settings.ApiToken = apiToken;
        }

        public CloudflareQueuesHostSettings Settings => _settings;
    }
}
