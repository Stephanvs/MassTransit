
namespace MassTransit.Configuration
{
    public interface ICloudflareQueuesHostConfigurator
    {
        void AccountId(string accountId);

        void AccountHash(string accountHash);

        void ApiToken(string apiToken);
    }
}
