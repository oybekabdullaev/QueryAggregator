using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using System.Xml.Linq;
using QueryAggregator.Core.Domain;

namespace QueryAggregator.Apis
{
    public class YandexApiHelper : IApiHelper
    {
        private readonly HttpClient _httpClient;

        public YandexApiHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Link>> GetLinksAsync(string query)
        {
            var url = GetRequestMessage(query);

            var response = await LoadAsync(url);

            return ParseResponse(response);
        }

        private HttpRequestMessage GetRequestMessage(string query)
        {
            var user = ConfigurationManager.AppSettings["YandexUser"];
            var key = ConfigurationManager.AppSettings["YandexKey"];

            if (user == null || key == null)
                throw new Exception("Please, provide Yandex key and user id");

            var url = $"https://yandex.com/search/xml?user={user}&key={key}&query={query}&l10n=en&sortby=rlv&filter=none&maxpassages=1&groupby=attr%3D%22%22.mode%3Dflat.groups-on-page%3D10.docs-in-group%3D1&page=1";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

            return request;
        }

        private async Task<string> LoadAsync(HttpRequestMessage url)
        {
            using (var responseMessage = await _httpClient.SendAsync(url))
            {
                if (!responseMessage.IsSuccessStatusCode)
                    throw new Exception(responseMessage.ReasonPhrase);

                return await responseMessage.Content.ReadAsStringAsync();
            }
        }

        private List<Link> ParseResponse(string response)
        {
            var docs = XDocument.Parse(response).Descendants("doc");

            try
            {
                return docs.Select(doc => new Link()
                {
                    Url = doc.Element("url").Value,
                    Title = doc.Element("title").Value,
                    Description = doc.Element("headline")?.Value
                }).ToList();
            }
            catch
            {
                throw new Exception("Cannot convert xml to link object.");
            }
        }
    }
}