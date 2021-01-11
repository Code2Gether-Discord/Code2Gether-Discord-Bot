using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Static;
using Discord;
using Discord.Commands;
using GitHubApiWrapper;
using GitHubApiWrapper.Models;
using Serilog;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class GitHubJoinTeamLogic : BaseLogic
    {
        private readonly string _githubAuthToken;
        private readonly string _githubOrganizationName;
        private readonly string _teamSlug;
        private readonly string _username;

        public GitHubJoinTeamLogic(ILogger logger, ICommandContext context, string githubAuthToken, string githubOrganizationName, string args) : base(logger, context)
        {
            _githubAuthToken = githubAuthToken;
            _githubOrganizationName = githubOrganizationName;

            var parsedArguments = ParseCommandArguments.ParseBy(' ', args);
            _teamSlug = parsedArguments.FirstOrDefault();
            _username = parsedArguments.LastOrDefault();
        }

        public override async Task<Embed> ExecuteAsync()
        {
            var result = await JoinGitHubTeam();

            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle(result.Title)
                .WithDescription(result.Description)
                .WithAuthor(_context.Message.Author)
                .Build();

            return embed;
        }

        private async Task<EmbedContent> JoinGitHubTeam()
        {
            var githubClient = new GitHubClient(_githubAuthToken);
            var response = await githubClient.AddOrUpdateTeamMembershipForUserAsync(_githubOrganizationName, _teamSlug, _username, Team.Role.member);
            return ParseHttpResponseToEmbedContent(response);
        }

        public EmbedContent ParseHttpResponseToEmbedContent(HttpResponseMessage response)
        {
            var embedContent = new EmbedContent
            {
                Title = "GitHub Join Team: "
            };

            if (response.IsSuccessStatusCode)
            {
                embedContent.Title += "Successful";
                embedContent.Description = $"Welcome to Team {_teamSlug}, {_context.User.Mention}!";
            }
            else
            {
                var errMsg =
                    $"There was an error requesting to join the team! GitHub's reason was: {response.ReasonPhrase}";

                _logger.Error(errMsg);

                embedContent.Title += "Failed";
                embedContent.Description = errMsg;
            }

            return embedContent;
        }
    }
}
