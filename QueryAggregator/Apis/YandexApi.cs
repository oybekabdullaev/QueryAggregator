using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using System.Xml.Linq;
using QueryAggregator.Core.Domain;

namespace QueryAggregator.Apis
{
    public class YandexApi : IApi
    {
        private readonly HttpClient _httpClient;

        public YandexApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Link>> GetLinksAsync(string query)
        {
            var url = GetUrl(query);

            var response = await LoadAsync(url);

            return ParseResponse(response);
        }

        private string GetUrl(string query)
        {
            var user = ConfigurationManager.AppSettings["YandexUser"];
            var key = ConfigurationManager.AppSettings["YandexKey"];

            if (user == null || key == null)
                throw new Exception("Please, provide Yandex key and user id");

            return $"https://yandex.com/search/xml?user={user}&key={key}&query={query}&l10n=en&sortby=rlv&filter=none&maxpassages=1&groupby=attr%3D%22%22.mode%3Dflat.groups-on-page%3D10.docs-in-group%3D1&page=1";
        }

        private async Task<string> LoadAsync(string url)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();

            using (var responseMessage = await _httpClient.GetAsync(url))
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