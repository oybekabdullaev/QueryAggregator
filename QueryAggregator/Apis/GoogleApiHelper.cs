using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using QueryAggregator.Apis.Dtos;
using QueryAggregator.Core.Domain;

namespace QueryAggregator.Apis
{
    public class GoogleApiHelper : IApiHelper
    {
        private readonly HttpClient _httpClient;

        public GoogleApiHelper(HttpClient httpClient)
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
            var key = ConfigurationManager.AppSettings["GoogleKey"];
            var searchEngineId = ConfigurationManager.AppSettings["GoogleSearchEngineId"];

            if (key == null || searchEngineId == null)
                throw new Exception("Please, provide Google key and search engine id.");

            return $"https://www.googleapis.com/customsearch/v1?key={key}&cx={searchEngineId}&q={query}&num=10";
        }

        private async Task<GoogleResponse> LoadAsync(string url)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();

            using (var responseMessage = await _httpClient.GetAsync(url))
            {
                if (!responseMessage.IsSuccessStatusCode)
                    throw new Exception(responseMessage.ReasonPhrase);

                return await responseMessage.Content.ReadAsAsync<GoogleResponse>();
            }
        }

        private List<Link> ParseResponse(GoogleResponse response)
        {
            return MapDtoToDomain(response.Items);
        }

        private List<Link> MapDtoToDomain(List<GoogleLink> response)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<GoogleLink, Link>()
                    .ForMember("Url", opt =>
                        opt.MapFrom(c => c.Link))
                    .ForMember("Title", opt =>
                        opt.MapFrom(c => c.Title))
                    .ForMember("Description", opt =>
                        opt.MapFrom(c => c.Snippet)));

            return new Mapper(config).Map<List<Link>>(response);
        }
    }
}