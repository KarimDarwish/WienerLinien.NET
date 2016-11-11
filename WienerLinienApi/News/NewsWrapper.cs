using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WienerLinienApi.RealtimeData;
using static WienerLinienApi.Model.Parameters;

namespace WienerLinienApi.News
{
    public class NewsWrapper
    {
        private const string NewsApiLink =
            "http://www.wienerlinien.at/ogd_realtime/newsList?{0}{1}{2}&sender={3}";

        private HttpClient _client;
        private readonly string _apiKey;

        public NewsWrapper(WienerLinienContext context)
        {
            if (context.ApiKey == null) return;
            _apiKey = context.ApiKey;
            _client = new HttpClient();
        }
        public async Task<Model.News> GetNewsInformationListAsync(NewsParameters parameters)
        {
            #region "Parameter check"
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            #endregion
            var url = parameters.GetStringFromParameters(NewsApiLink, _apiKey);
            if (_client == null)
                _client = new HttpClient();

            var response =  await _client.GetStringAsync(url).ConfigureAwait(false);
          
            var deserialized = JsonConvert.DeserializeObject<Model.News>(response);
            if (deserialized.MessageObj != null)
            {
                throw new RealtimeError(deserialized.MessageObj.MessageCode);
            }

            return response != null ? JsonConvert.DeserializeObject<Model.News>(response) : null;

        }

        public async Task<Model.News> GetNewsInformationAsync(NewsParameters parameters)
        {
            #region "Parameter check"
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            #endregion
            var url = parameters.GetStringFromParameters(NewsApiLink, _apiKey);
            if (_client == null)
                _client = new HttpClient();

            var response = await _client.GetStringAsync(url).ConfigureAwait(false);
            return response != null ? JsonConvert.DeserializeObject<Model.News>(response) : null;
        }
    }
}