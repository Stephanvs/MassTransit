namespace MassTransit
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Configuration;
    using Contexts;


    public class CloudflareQueuesReceiveTransport : IReceiveTransport
    {
        private readonly CloudflareQueuesHostSettings _hostSettings;
        private readonly CloudflareQueuesReceiveEndpointSettings _receiveSettings;
        private CancellationTokenSource _cancellationTokenSource;

        public CloudflareQueuesReceiveTransport(CloudflareQueuesHostSettings hostSettings, CloudflareQueuesReceiveEndpointSettings receiveSettings)
        {
            _hostSettings = hostSettings;
            _receiveSettings = receiveSettings;
            _receiveObservers = new ReceiveObservable();
            _receiveTransportObservers = new ReceiveTransportObservable();
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("CloudflareQueuesReceiveTransport");
        }

        public async Task Start(IReceivePipe receivePipe)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            var client = new HttpClient();
            client.BaseAddress = _hostSettings.ServiceUri;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _hostSettings.ApiToken);

            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                var response = await client.PostAsync($"{_receiveSettings.QueueName}/messages/pull", new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(new { batch_size = _receiveSettings.BatchSize, visibility_timeout = _receiveSettings.VisibilityTimeout }), System.Text.Encoding.UTF8, "application/json"), _cancellationTokenSource.Token);

                var content = await response.Content.ReadAsStringAsync();

                dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                if (result.messages != null)
                {
                    foreach (var message in result.messages)
                    {
                        var body = System.Convert.FromBase64String((string)message.body);
                        var headers = new Dictionary<string, object>();
                        if (message.metadata != null)
                        {
                            foreach (var property in message.metadata.Children<Newtonsoft.Json.Linq.JProperty>())
                            {
                                headers[property.Name] = property.Value.ToObject<object>();
                            }
                        }

                        var context = new CloudflareQueuesReceiveContext(body, (int)message.attempts > 1, new CloudflareQueuesHeaderProvider(headers));

                        await receivePipe.Send(context);
                    }
                }

                await Task.Delay(1000, _cancellationTokenSource.Token);
            }
        }

        public Task Stop()
        {
            _cancellationTokenSource?.Cancel();

            return Task.CompletedTask;
        }

        private readonly ReceiveObservable _receiveObservers;
        private readonly ReceiveTransportObservable _receiveTransportObservers;

        public ConnectHandle ConnectReceiveObserver(IReceiveObserver observer)
        {
            return _receiveObservers.Connect(observer);
        }

        public ConnectHandle ConnectReceiveTransportObserver(IReceiveTransportObserver observer)
        {
            return _receiveTransportObservers.Connect(observer);
        }
    }
}