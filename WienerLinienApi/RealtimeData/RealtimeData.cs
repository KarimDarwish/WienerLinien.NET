using System;
using System.Net.Http;
using System.Threading;
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
        private readonly string _apiKey;
        private HttpClient _client;

        public RealtimeData(WienerLinienContext context)
        {
            _client = new HttpClient();
            if (context.ApiKey == string.Empty) return;
            _apiKey = context.ApiKey;
            _client = context.Client;
        }
        public async Task<MonitorData> GetMonitorDataAsync(Parameters.MonitorParameters parameters)
        {
            #region "Parameter check"
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            #endregion

            var url = parameters.GetStringFromParameters(MonitorApiLink, _apiKey);
            if (_client == null)
                _client = new HttpClient();

            var response = await _client.GetStringAsync(url).ConfigureAwait(false);
            var deserialized = JsonConvert.DeserializeObject<MonitorData>(response);
            if (deserialized.Message != null && !deserialized.Message.Value.Equals("OK"))
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
            var url = parameters.GetStringFromParameters(TrafficInfoListApiLink, _apiKey);
            if (_client == null)
                _client = new HttpClient();
            var response = await _client.GetStringAsync(url).ConfigureAwait(false);
            var deserialized = JsonConvert.DeserializeObject<TrafficInfoData>(response);
            if (deserialized.Message != null)
            {
                throw new RealtimeError(deserialized.Message.MessageCode);
            }

            return response != null ? deserialized : null;
        }
    }


    #region "Parameters class"

    #endregion
}