namespace MassTransit
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Configuration;
    using Contexts;
    using MassTransit.Transports;


    public class CloudflareQueuesSendTransport : ISendTransport
    {
        private readonly CloudflareQueuesHostSettings _hostSettings;
        private readonly SendObservable _sendObservers;

        public CloudflareQueuesSendTransport(CloudflareQueuesHostSettings hostSettings)
        {
            _hostSettings = hostSettings;
            _sendObservers = new SendObservable();
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("CloudflareQueuesSendTransport");
        }

        public async Task Send<T>(T message, IPipe<SendContext<T>> pipe) where T : class
        {
            var client = new HttpClient();
            client.BaseAddress = _hostSettings.ServiceUri;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _hostSettings.ApiToken);

            var sendContext = new CloudflareQueuesSendContext<T>(message);
            await pipe.Send(sendContext);

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(sendContext.Message);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync("", content);
            response.EnsureSuccessStatusCode();
        }

        public ConnectHandle ConnectSendObserver(ISendObserver observer)
        {
            return _sendObservers.Connect(observer);
        }
    }
}