using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using QueryAggregator.Apis.Dtos;
using QueryAggregator.Core.Domain;

namespace QueryAggregator.Apis
{
    public class BingApiHelper : IApiHelper
    {
        private readonly HttpClient _httpClient;

        public BingApiHelper(HttpClient httpClient)
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
            var key = ConfigurationManager.AppSettings["BingKey"];

            if (string.IsNullOrWhiteSpace(key))
                throw new Exception("Please, provide Bing key.");

            var url = $"https://api.bing.microsoft.com/v7.0/search?q={query}&responseFilter=webpages&count=10";
            
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
            
            request.Headers.Add("Ocp-Apim-Subscription-Key", key);

            return request;
        }

        private async Task<BingResponse> LoadAsync(HttpRequestMessage request)
        {
            using (var responseMessage = await _httpClient.SendAsync(request))
            {
                if (responseMessage.IsSuccessStatusCode)
                    return await responseMessage.Content.ReadAsAsync<BingResponse>();

                throw new Exception("Bing error: " + responseMessage.ReasonPhrase);
            }
        }

        private List<Link> ParseResponse(BingResponse response)
        {
            return MapDtoToDomain(response.WebPages.Value);
        }

        private List<Link> MapDtoToDomain(List<BingLink> response)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<BingLink, Link>()
                    .ForMember("Url", opt =>
                        opt.MapFrom(c => c.Url))
                    .ForMember("Title", opt =>
                        opt.MapFrom(c => c.Name))
                    .ForMember("Description", opt =>
                        opt.MapFrom(c => c.Snippet)));

            var mapper = new Mapper(config);

            return mapper.Map<List<Link>>(response);
        }
    }
}