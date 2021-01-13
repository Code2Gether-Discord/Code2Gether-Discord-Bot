using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GitHubApiWrapper
{
    public class GitHubClient
    {
        public readonly HttpClient Client;

        public GitHubClient(string authenticationToken, string organizationName = "")
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("CustomClient", "1"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationToken);
            Client.BaseAddress = new Uri($"https://api.github.com/orgs/{organizationName}/");
        }
    }
}
