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

        public Task<HttpResponseMessage> GetAsync(string endpoint)
        {
            return _client.GetAsync(endpoint);
        }
        
        public Task<HttpResponseMessage> PutAsync(string endpoint, StringContent content)
        {
            return _client.PutAsync(endpoint, content);
        }

        public Task<HttpResponseMessage> PostAsync(string endpoint, StringContent content)
        {
            return _client.PostAsync(endpoint, content);
        }
    }
}
