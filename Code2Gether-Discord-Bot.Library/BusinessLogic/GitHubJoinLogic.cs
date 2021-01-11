using System.Net.Http;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.CustomExceptions;
using Discord;
using Discord.Commands;
using GitHubApiWrapper;
using Serilog;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class GitHubJoinLogic : BaseLogic
    {
        private const string GITHUB_ORG_NAME = "Code2Gether-Discord";
        private readonly string _githubAuthToken;
        private readonly string _userGitHubEmail;

        public GitHubJoinLogic(ILogger logger, ICommandContext context, string githubAuthToken, string userGitHubEmail) : base(logger, context)
        {
            _githubAuthToken = githubAuthToken;
            _userGitHubEmail = userGitHubEmail.Trim();

            if (!HttpHelper.IsValidEmail(_userGitHubEmail))
            {
                throw new InvalidEmailException($"{_userGitHubEmail} is not a valid email");
            }
        }
        
        public override async Task<Embed> ExecuteAsync()
        {
            var result = await JoinGitHubOrganization();

            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle(result.Title)
                .WithDescription(result.Description)
                .WithAuthor(_context.Message.Author)
                .Build();

            return embed;
        }

        private async Task<EmbedContent> JoinGitHubOrganization()
        {
            var githubClient = new GitHubClient(_githubAuthToken);
            var response = await githubClient.InviteViaEmailToOrganizationAsync(GITHUB_ORG_NAME, _userGitHubEmail);
            return ParseHttpResponseToEmbedContent(response);
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
                    $"There was an error requesting to join the organization! GitHub's reason was: {response.ReasonPhrase}";

                _logger.Error(errMsg);

                embedContent.Title += "Failed";
                embedContent.Description = errMsg;
            }

            return embedContent;
        }
    }
}
