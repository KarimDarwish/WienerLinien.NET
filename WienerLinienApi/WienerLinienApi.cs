namespace WienerLinienApi
{
    public class WienerLinienApi
    {
        public string ApiKey { get; private set; }
        public RealtimeData.RealtimeData RealtimeData { get; set; }

        public WienerLinienApi(string apiKey)
        {
            ApiKey = apiKey;
        }
       
    }
}
