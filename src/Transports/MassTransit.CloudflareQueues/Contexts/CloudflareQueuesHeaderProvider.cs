
namespace MassTransit.Contexts
{
    using System.Collections.Generic;
    using MassTransit.Transports;


    public class CloudflareQueuesHeaderProvider : IHeaderProvider
    {
        private readonly IDictionary<string, object> _headers;

        public CloudflareQueuesHeaderProvider(IDictionary<string, object> headers)
        {
            _headers = headers;
        }

        public IEnumerable<KeyValuePair<string, object>> GetAll()
        {
            return _headers;
        }

        public bool TryGetHeader(string key, out object value)
        {
            return _headers.TryGetValue(key, out value);
        }
    }
}
