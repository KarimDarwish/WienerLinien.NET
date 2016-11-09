namespace WienerLinienApi.RealtimeData
{
    public interface IParameter
    {
         string GetStringFromParameters(string url, string apiKey);
    }
}
