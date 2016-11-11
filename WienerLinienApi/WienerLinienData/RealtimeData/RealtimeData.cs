using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WienerLinienApi.RealtimeData.Monitor;
using WienerLinienApi.RealtimeData.TrafficInfo;

namespace WienerLinienApi.RealtimeData
{
    internal class RealtimeData
    {

        private const string MonitorApiLink = "https://www.wienerlinien.at/ogd_realtime/monitor?{0}{1}&sender={2}";
        private const string TrafficInfoListApiLink = "https://www.wienerlinien.at/ogd_realtime/trafficInfoList?{0}{1}{2}&sender={3}";
        private const string TrafficInfoApiLink = "https://www.wienerlinien.at/ogd_realtime/trafficInfo?{0}{1}{2}&sender={3}";
        public string ApiKey { get; private set; }
        private HttpClient client;

        public RealtimeData(string apiKey)
        {
            client = new HttpClient();
            if (apiKey != string.Empty)
            {
                ApiKey = apiKey;
            }
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
            return response != null ? JsonConvert.DeserializeObject<MonitorData>(response) : null;
        }

        public async Task<TrafficInfoData> GetTrafficInfoListDataAsync(Parameters.TrafficInfoParameters parameters)
        {
            #region "Parameter check"
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            #endregion
            var url = parameters.GetStringFromParameters(TrafficInfoListApiLink, ApiKey);
            if (client == null)
                client = new HttpClient();

            var response = await client.GetStringAsync(url).ConfigureAwait(false);
            return response != null ? JsonConvert.DeserializeObject<TrafficInfoData>(response) : null;
        }
        public async Task<TrafficInfoData> GetTrafficInfoDataAsync(Parameters.TrafficInfoParameters parameters)
        {
            #region "Parameter check"
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            #endregion
            var url = parameters.GetStringFromParameters(TrafficInfoApiLink, ApiKey);
            if (client == null)
                client = new HttpClient();

            var response = await client.GetStringAsync(url).ConfigureAwait(false);
            return response != null ? JsonConvert.DeserializeObject<TrafficInfoData>(response) : null;
        }


    }
   

    #region "Parameters class"
    public class Parameters
    {
        public class MonitorParameters: IParameter
        {
            /// <summary>
            /// List of all RBL's you want to receive real time data for
            /// </summary>
            public List<int> Rbls { get; set; }
            /// <summary>
            /// Parameters for traffic information
            /// </summary>
            public enum TrafficInfo { Stoerungkurz, Stoerunglang, AufzugsInfo }
            public List<TrafficInfo> TrafficInformation { get; set; }

            public string GetStringFromParameters(string url, string apiKey)
            {
                var rbls = string.Join("&", Rbls.Select(r => $"rbl={r}"));
                var trafficInfo = new List<string>() {""};
                if (TrafficInformation != null && TrafficInformation.Count != 0)
                {
                    trafficInfo.AddRange(TrafficInformation.Select(item => "&activateTrafficInfo=" + item.ToString().ToLower()));
                }

                var result = string.Join("", trafficInfo.ToArray());
                return string.Format(url, rbls, result, apiKey);
            }
        }

        public class TrafficInfoParameters: IParameter
        {
            public List<string> RelatedLines { get; set; }
            public List<string> RelatedStops { get; set; }
            public enum TrafficInfo { Stoerungkurz, Stoerunglang, AufzugsInfo }
            public List<TrafficInfo> TrafficInformation { get; set; }

            public string GetStringFromParameters(string url, string apiKey)
            {

                var relatedLines = string.Empty;
                var relatedStops = string.Empty;
                if (RelatedLines != null)
                {
                    relatedLines = string.Join("&", RelatedLines.Select(r => $"relatedLine={r}"));
                }

                if (RelatedStops != null)
                {
                    relatedStops = string.Join("&", RelatedStops.Select(r => $"relatedStop={r}"));
                }
                var trafficInfo = new List<string>();
                
                if (TrafficInformation != null && TrafficInformation.Count != 0)
                {
                    trafficInfo.AddRange(
                        TrafficInformation.Select(item => "&activateTrafficInfo=" + item.ToString().ToLower()));
                }
                var result = string.Join("", trafficInfo.ToArray());

                return string.Format(url, relatedLines, relatedStops, result, apiKey);
            }
        }

    }
#endregion
}
