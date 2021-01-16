using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Discord;
using Discord.Commands;
using GitHubApiWrapper;
using Newtonsoft.Json.Linq;
using Serilog;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class GitHubListTeamMembersLogic : BaseLogic
    {
        private readonly GitHubClient _client;
        private readonly string _gitHubTeamSlug;

        public GitHubListTeamMembersLogic(ILogger logger, ICommandContext context, GitHubClient client, string gitHubTeamSlug) : base(logger, context)
        {
            _client = client;
            _gitHubTeamSlug = gitHubTeamSlug;
        }

        public override async Task<Embed> ExecuteAsync()
        {
            var result = await ParseHttpResponseToEmbedContentAsync(await GetTeamMembersAsync());

            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle(result.Title)
                .WithDescription(result.Description)
                .WithAuthor(_context.Message.Author)
                .Build();

            return embed;
        }

        private async Task<HttpResponseMessage> GetTeamMembersAsync()
        {
            var response = await _client.Client.GetAsync($"teams/{_gitHubTeamSlug}/members");
            return response;
        }

        public async Task<EmbedContent> ParseHttpResponseToEmbedContentAsync(HttpResponseMessage response)
        {
            var embedContent = new EmbedContent
            {
                Title = "GitHub List Team Members"
            };

            if (response.IsSuccessStatusCode)
            {
                var descriptionSb = new StringBuilder();
                var json = await response.Content.ReadAsStringAsync();
                var teamMembersJToken = JToken.Parse(json);
                var count = teamMembersJToken.Count();

                descriptionSb.Append($"**Total Members on Team {_gitHubTeamSlug}: {count}**{Environment.NewLine}");

                for (var i = 0; i < count; i++)
                {
                    descriptionSb.Append($"{i + 1}: {teamMembersJToken[i]["login"]} ({teamMembersJToken[i]["url"]}){Environment.NewLine}");
                }

                embedContent.Title += "Successful";
                embedContent.Description = descriptionSb.ToString();
            }
            else
            {
                var errMsg =
                    $"There was an error requesting to list the Team's Members! GitHub's reason was: {response.ReasonPhrase}";

                _logger.Error(errMsg);

                embedContent.Title += ": Failed";
                embedContent.Description = errMsg;
            }

            return embedContent;
        }
    }
}
