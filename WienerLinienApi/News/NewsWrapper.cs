using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static WienerLinienApi.Model.Parameters;

namespace WienerLinienApi.News
{
    public class NewsWrapper
    {
        private const string NewsApiLink =
            "http://www.wienerlinien.at/ogd_realtime/newsList?{0}{1}{2}&sender={3}";

        private HttpClient client;
        private readonly string apiKey;

        public NewsWrapper(WienerLinienContext context)
        {
            if (context.ApiKey == null) return;
            apiKey = context.ApiKey;
            client = new HttpClient();
        }
        public async Task<Model.News> GetNewsInformationListAsync(NewsParameters parameters)
        {
            #region "Parameter check"
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            #endregion
            var url = parameters.GetStringFromParameters(NewsApiLink, apiKey);
            if (client == null)
                client = new HttpClient();

            var response =  await client.GetStringAsync(url).ConfigureAwait(false);
            var data = JsonConvert.DeserializeObject<Model.News>(response.ToString());


            return response != null ? JsonConvert.DeserializeObject<Model.News>(response) : null;

        }

        public async Task<Model.News> GetNewsInformationAsync(NewsParameters parameters)
        {
            #region "Parameter check"
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            #endregion
            var url = parameters.GetStringFromParameters(NewsApiLink, apiKey);
            if (client == null)
                client = new HttpClient();

            var response = await client.GetStringAsync(url).ConfigureAwait(false);
            return response != null ? JsonConvert.DeserializeObject<Model.News>(response) : null;
        }
    }
}