using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.CustomExceptions;
using Discord;
using Discord.Commands;
using GitHubApiWrapper;
using Serilog;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class GitHubJoinOrgLogic : BaseLogic
    {
        private readonly string _githubAuthToken;
        private readonly string _githubOrganizationName;
        private readonly string _userGitHubEmail;

        public GitHubJoinOrgLogic(ILogger logger, ICommandContext context, string githubAuthToken, string githubOrganizationName, string userGitHubEmail) : base(logger, context)
        {
            _githubAuthToken = githubAuthToken;
            _githubOrganizationName = githubOrganizationName;
            _userGitHubEmail = userGitHubEmail.Trim();

            if (!HttpHelper.IsValidEmail(_userGitHubEmail))
            {
                throw new InvalidEmailException($"{_userGitHubEmail} is not a valid email");
            }
        }
        
        public override async Task<Embed> ExecuteAsync()
        {
            var result = ParseHttpResponseToEmbedContent(await PostNewInviteToOrganization());

            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle(result.Title)
                .WithDescription(result.Description)
                .WithAuthor(_context.Message.Author)
                .Build();

            return embed;
        }

        private async Task<HttpResponseMessage> PostNewInviteToOrganization()
        {
            var githubClient = new GitHubClient(_githubAuthToken, _githubOrganizationName);

            var userData = new Dictionary<string, string>
            {
                ["email"] = _userGitHubEmail
            };

            var content = new StringContent(JsonSerializer.Serialize(userData));
            var response = await githubClient.PostAsync($"invitations", content);

            return response;
        }

        public EmbedContent ParseHttpResponseToEmbedContent(HttpResponseMessage response)
        {
            var embedContent = new EmbedContent
            {
                Title = "GitHub Join: "
            };

            if (response.IsSuccessStatusCode)
            {
                embedContent.Title += "Successful";
                embedContent.Description = $"Welcome to the Organization, {_context.User.Mention}!";
            }
            else
            {
                var errMsg =
                    $"There was an error requesting to join the Organization! GitHub's reason was: {response.ReasonPhrase}";

                _logger.Error(errMsg);

                embedContent.Title += "Failed";
                embedContent.Description = errMsg;
            }

            return embedContent;
        }
    }
}
