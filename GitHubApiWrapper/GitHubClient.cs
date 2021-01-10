using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GitHubApiWrapper.Models;
using GitHubApiWrapper.Models.CustomExceptions;

namespace GitHubApiWrapper
{
    public class GitHubClient
    {
        private readonly HttpClient _client;

        public GitHubClient(string authenticationToken)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("CustomClient", "1"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationToken);
        }

        public async Task<HttpResponseMessage> InviteViaEmailToOrganizationAsync(string organizationName, string email)
        {
            try
            {
                var organization = new Organization(organizationName);

                var content = new StringContent($"{{\"email\": \"{email}\"}}");
                content.Headers.ContentType.MediaType = "application/json";

                return await _client.PostAsync(organization.InvitationsEndpoint, content);
            }
            catch (Exception e)
            {
                throw new GitHubClientException($"Failed to invite {email} to the organization {organizationName}!", e);
            }
        }
    }
}
