using System;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Discord;
using Discord.Commands;
using GitHubApiWrapper;
using Newtonsoft.Json.Linq;
using Serilog;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class GitHubListTeamsLogic : BaseLogic
    {
        private readonly GitHubClient _client;

        public GitHubListTeamsLogic(ILogger logger, ICommandContext context, GitHubClient client) : base(logger, context)
        {
            _client = client;
        }

        public override async Task<Embed> ExecuteAsync()
        {
            var result = await ParseHttpResponseToEmbedContentAsync(await GetOrganizationTeamsAsync());

            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle(result.Title)
                .WithDescription(result.Description)
                .WithAuthor(_context.Message.Author)
                .Build();

            return embed;
        }

        private async Task<HttpResponseMessage> GetOrganizationTeamsAsync()
        {
            var response = await _client.Client.GetAsync($"teams");
            return response;
        }

        public async Task<EmbedContent> ParseHttpResponseToEmbedContentAsync(HttpResponseMessage response)
        {
            var embedContent = new EmbedContent
            {
                Title = "GitHub List Teams: "
            };

            if (response.IsSuccessStatusCode)
            {
                var descriptionSb = new StringBuilder();
                var json = await response.Content.ReadAsStringAsync();
                var teamsJToken = JToken.Parse(json);
                var count = teamsJToken.Count();

                descriptionSb.Append($"**Total projects: {count}**{Environment.NewLine}");

                for (var i = 0; i < count; i++)
                {
                    descriptionSb.Append($"{i+1}: {teamsJToken[i]["slug"]}{Environment.NewLine}");
                }

                embedContent.Title += "Successful";
                embedContent.Description = descriptionSb.ToString();
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
