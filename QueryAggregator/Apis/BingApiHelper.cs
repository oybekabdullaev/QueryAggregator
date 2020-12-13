using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
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
            var url = GetUrl(query);

            var response = await LoadAsync(url);

            return ParseResponse(response);
        }

        private string GetUrl(string query)
        {
            return $"https://api.bing.microsoft.com/v7.0/search?q={query}&responseFilter=webpages&count=10";
        }

        private async Task<BingResponse> LoadAsync(string url)
        {
            ConfigureHttpClientHeaders();

            using (var responseMessage = await _httpClient.GetAsync(url))
            {
                if (!responseMessage.IsSuccessStatusCode)
                    throw new Exception(responseMessage.ReasonPhrase);

                return await responseMessage.Content.ReadAsAsync<BingResponse>();
            }
        }

        private void ConfigureHttpClientHeaders()
        {
            var key = ConfigurationManager.AppSettings["BingKey"];

            if (key == null)
                throw new Exception("Please, provide Bing key.");

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);
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