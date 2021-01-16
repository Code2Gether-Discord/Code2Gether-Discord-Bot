using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Static;
using Discord;
using Discord.Commands;
using GitHubApiWrapper;
using Serilog;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class GitHubJoinTeamLogic : BaseLogic
    {
        private readonly GitHubClient _client;
        private readonly string _teamSlug;
        private readonly string _username;

        public GitHubJoinTeamLogic(ILogger logger, ICommandContext context, GitHubClient client, string args) : base(logger, context)
        {
            _client = client;
            var parsedArguments = ParseCommandArguments.ParseBy(' ', args);
            _teamSlug = parsedArguments.FirstOrDefault();
            _username = parsedArguments.LastOrDefault();
        }

        public override async Task<Embed> ExecuteAsync()
        {
            var result = ParseHttpResponseToEmbedContent(await PutNewMemberToTeamAsync());

            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle(result.Title)
                .WithDescription(result.Description)
                .WithAuthor(_context.Message.Author)
                .Build();

            return embed;
        }

        private async Task<HttpResponseMessage> PutNewMemberToTeamAsync()
        {
            var defaultRole = new Dictionary<string, string>
            {
                ["role"] = "member"
            };

            var content = new StringContent(JsonSerializer.Serialize(defaultRole));
            var response = await _client.PutAsync($"teams/{_teamSlug}/members/{_username}", content);

            return response;
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
                    $"There was an error requesting to join the Organization's Team! GitHub's reason was: {response.ReasonPhrase}";

                _logger.Error(errMsg);

                embedContent.Title += "Failed";
                embedContent.Description = errMsg;
            }

            return embedContent;
        }
    }
}
