using System.Net.Http;

namespace WienerLinienApi
{
    public class WienerLinienContext
    {
        public string ApiKey { get; }
        public HttpClient Client { get; }

        public WienerLinienContext(string apiKey)
        {
            ApiKey = apiKey;
            Client = new HttpClient();
        }
       
    }
}
