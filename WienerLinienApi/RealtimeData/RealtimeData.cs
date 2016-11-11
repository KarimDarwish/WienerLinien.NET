using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WienerLinienApi.Model;
using WienerLinienApi.RealtimeData.Monitor;
using WienerLinienApi.RealtimeData.TrafficInfo;

namespace WienerLinienApi.RealtimeData
{
    public class RealtimeData
    {

        private const string MonitorApiLink = "https://www.wienerlinien.at/ogd_realtime/monitor?{0}{1}&sender={2}";
        private const string TrafficInfoListApiLink = "https://www.wienerlinien.at/ogd_realtime/trafficInfoList?{0}{1}{2}&sender={3}";
        private const string TrafficInfoApiLink = "https://www.wienerlinien.at/ogd_realtime/trafficInfo?{0}{1}{2}&sender={3}";
        public string ApiKey { get; private set; }
        private HttpClient client;

        public RealtimeData(WienerLinienContext context)
        {
            client = new HttpClient();
            if (context.ApiKey == string.Empty) return;
            ApiKey = context.ApiKey;
            client = context.Client;
        }
        public async Task<MonitorData> GetMonitorDataAsync(Parameters.MonitorParameters parameters)
        {
            #region "Parameter check"
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            #endregion

            var url = parameters.GetStringFromParameters(MonitorApiLink, ApiKey);
            if (client == null)
                client = new HttpClient();

            var response = await client.GetStringAsync(url).ConfigureAwait(false);
            var deserialized = JsonConvert.DeserializeObject<MonitorData>(response);
            if (deserialized.Message != null)
            {
                throw new RealtimeError(deserialized.Message.MessageCode);
            }
            return response != null ? deserialized : null;
        }

        public async Task<TrafficInfoData> GetTrafficInfoDataAsync(Parameters.TrafficInfoParameters parameters)
        {
            #region "Parameter check"
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            #endregion
            var url = parameters.GetStringFromParameters(TrafficInfoListApiLink, ApiKey);
            if (client == null)
                client = new HttpClient();

            var response = await client.GetStringAsync(url).ConfigureAwait(false);
            var deserialized = JsonConvert.DeserializeObject<TrafficInfoData>(response);
            if (deserialized.Message != null)
            {
                throw new RealtimeError(deserialized.Message.MessageCode);
            }
           
            return response != null ?deserialized : null;
        }
     
    }


    #region "Parameters class"
   
    #endregion
}