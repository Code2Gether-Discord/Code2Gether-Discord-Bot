using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GitHubApiWrapper.Models;
using GitHubApiWrapper.Models.CustomExceptions;
using GitHubApiWrapper.Models.Members;
using GitHubApiWrapper.Models.Teams;

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

                var content = new StringContent($"{{\"email\":\"{email}\"}}");
                content.Headers.ContentType.MediaType = "application/json";

                return await _client.PostAsync(organization.GetEndpoint("invitations"), content);
            }
            catch (Exception e)
            {
                throw new GitHubClientException($"Failed to invite {email} to the organization {organizationName}!", e);
            }
        }

        public async Task<HttpResponseMessage> AddOrUpdateTeamMembershipForUserAsync(string organizationName, string teamSlug, string username, string role)
        {
            try
            {
                var organization = new Organization(organizationName);

                var teams = await GetOrganizationTeamsAsync(organization);
                var requestedTeam = teams.First(t => t.Slug.Equals(teamSlug));

                var content = new StringContent($"{{\"role\":\"{role}\"}}");
                content.Headers.ContentType.MediaType = "application/json";

                return await _client.PutAsync($"{requestedTeam.MembersUrl}\\{username}", content);
            }
            catch (Exception e)
            {
                throw new GitHubClientException($"Failed to add or update {username} for the team with the slug {teamSlug}!", e);
            }
        }

        public async Task<List<Members>> GetOrganizationMembers(string organizationName)
        {
            var organization = new Organization(organizationName);

            var membersResponse= await _client.GetAsync(organization.GetEndpoint("members"));
            var members = Members.FromJson(membersResponse.Content.ToString());

            return members;
        }

        public async Task<List<Members>> GetOrganizationTeamMembers(string organizationName, string teamSlug)
        {
            var organization = new Organization(organizationName);

            var teams = await GetOrganizationTeamsAsync(organization);
            var requestedTeam = teams.First(t => t.Slug.Equals(teamSlug));
            var teamMembersResponse = await _client.GetAsync(requestedTeam.MembersUrl);
            var teamMembers = Members.FromJson(teamMembersResponse.Content.ToString());

            return teamMembers;
        }

        private async Task<List<Teams>> GetOrganizationTeamsAsync(Organization organization)
        {
            var teamsResponse = await _client.GetAsync(organization.GetEndpoint("teams"));
            var teams = Teams.FromJson(teamsResponse.Content.ToString());
            return teams;
        }
    }
}
