namespace MassTransit.Contexts
{
    using System.IO;
    using System.Text;

    public class CloudflareQueuesMessageBody : MessageBody
    {
        private readonly byte[] _body;

        public CloudflareQueuesMessageBody(byte[] body)
        {
            _body = body;
        }

        public long? Length => _body.Length;

        public Stream GetStream()
        {
            return new MemoryStream(_body, false);
        }

        public byte[] GetBytes()
        {
            return _body;
        }

        public string GetString()
        {
            return Encoding.UTF8.GetString(_body);
        }
    }
}
