using System.Collections.Generic;
using System.Linq;
using WienerLinienApi.RealtimeData;

namespace WienerLinienApi.Model
{
    public class Parameters
    {
        public class MonitorParameters : IParameter
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
                var trafficInfo = new List<string>() { "" };
                if (TrafficInformation != null && TrafficInformation.Count != 0)
                {
                    trafficInfo.AddRange(TrafficInformation.Select(item => "&activateTrafficInfo=" + item.ToString().ToLower()));
                }

                var result = string.Join("", trafficInfo.ToArray());
                return string.Format(url, rbls, result, apiKey);
            }
        }

        public class TrafficInfoParameters : IParameter
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
        public class NewsParameters : IParameter
        {
            public List<string> RelatedLines { get; set; }
            public List<string> RelatedStops { get; set; }

            public List<NewsCategories> Names { get; set; }
            public enum NewsCategories { News, Aufzugsservice }

            public string GetStringFromParameters(string url, string apiKey)
            {
                var relatedLines = string.Empty;
                var relatedStops = string.Empty;
                if (RelatedLines != null && RelatedLines.Count != 0)
                {
                    relatedLines = string.Join("&", RelatedLines.Select(r => $"relatedLine={r}"));
                }
                if (RelatedStops != null && RelatedStops.Count != 0)
                {
                    relatedStops = string.Join("&", RelatedStops.Select(r => $"relatedStop={r}"));
                }
                var names = new List<string>() { "" };
                if (Names != null && Names.Count != 0)
                {
                    names.AddRange(Names.Select(item => "&name=" + item.ToString().ToLower()));
                }

                var result = string.Join("", names.ToArray());
                return string.Format(url, relatedLines, relatedStops, result, apiKey);
            }
        }

    }
}
