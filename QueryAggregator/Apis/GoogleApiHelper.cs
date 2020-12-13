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
            var request = GetRequestMessage(query);

            var response = await LoadAsync(request);

            return ParseResponse(response);
        }

        private HttpRequestMessage GetRequestMessage(string query)
        {
            var key = ConfigurationManager.AppSettings["GoogleKey"];
            var searchEngineId = ConfigurationManager.AppSettings["GoogleSearchEngineId"];

            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(searchEngineId))
                throw new Exception("Please, provide Google key and search engine id.");

            var url = $"https://www.googleapis.com/customsearch/v1?key={key}&cx={searchEngineId}&q={query}&num=10";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            return request;
        }

        private async Task<GoogleResponse> LoadAsync(HttpRequestMessage request)
        {
            using (var responseMessage = await _httpClient.SendAsync(request))
            {
                if (responseMessage.IsSuccessStatusCode)
                    return await responseMessage.Content.ReadAsAsync<GoogleResponse>();
                
                var response = await responseMessage.Content.ReadAsAsync<GoogleErrorResponse>();
                throw new Exception("Google error: " + response.Error.Message);
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