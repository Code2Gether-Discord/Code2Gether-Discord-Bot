using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GitHubApiWrapper
{
    public class GitHubClient
    {
        private readonly HttpClient _client;

        public GitHubClient(string authenticationToken, string organizationName = "")
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("CustomClient", "1"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationToken);
            _client.BaseAddress = new Uri($"https://api.github.com/orgs/{organizationName}/");
        }
        public async Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            return await _client.GetAsync(endpoint);
        }
        
        public async Task<HttpResponseMessage> PutAsync(string endpoint, StringContent content)
        {
            return await _client.PutAsync(endpoint, content);
        }
    }
}
