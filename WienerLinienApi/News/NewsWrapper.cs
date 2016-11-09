using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WienerLinienApi.News
{
    public class NewsWrapper
    {
        private const string NewsApiLink =
            "http://www.wienerlinien.at/ogd_realtime/newsList?{0}{1}{2}&sender={3}";

        private HttpClient client;

        public NewsWrapper()
        {
            
        }
        //public Task<List<Model.News>> GetNewsInformationAsync()
        //{
            
        //}
    }

    public class Parameters
    {
        public List<string> RelatedLines { get; set; }    
        public List<string> RelatedStops { get; set; }

        public List<NewsCategories> Names { get; set; }
        public enum NewsCategories { News, Aufzugsservice}

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
            return string.Format(url, relatedLines, relatedStops,result, apiKey);
        }
    }
}
