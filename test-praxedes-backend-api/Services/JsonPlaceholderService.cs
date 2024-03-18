using test_praxedes_backend_api.Contracts;
using test_praxedes_backend_api.Dto;

namespace test_praxedes_backend_api.Services
{
    public class JsonPlaceholderService : IJsonPlaceholderService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public JsonPlaceholderService(HttpClient httpClient,
            IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<List<JsonPlaceholderPostDto>> GetPosts()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, configuration.GetValue<string>("PathJsonPlaceholderPost"));
            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<JsonPlaceholderPostDto>>();
            }
            return new List<JsonPlaceholderPostDto>();
        }

        public async Task<List<JsonPlaceholderCommentDto>> GetComments()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, configuration.GetValue<string>("PathJsonPlaceholderComment"));
            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<JsonPlaceholderCommentDto>>();
            }
            return new List<JsonPlaceholderCommentDto>();
        }
    }
}

