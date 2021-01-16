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
    public class GitHubListOrgLogic : BaseLogic
    {
        private readonly GitHubClient _client;

        public GitHubListOrgLogic(ILogger logger, ICommandContext context, GitHubClient client) : base(logger, context)
        {
            _client = client;
        }

        public override async Task<Embed> ExecuteAsync()
        {
            var result = await ParseHttpResponseToEmbedContentAsync(await GetOrganizationMembersAsync());

            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle(result.Title)
                .WithDescription(result.Description)
                .WithAuthor(_context.Message.Author)
                .Build();

            return embed;
        }

        private async Task<HttpResponseMessage> GetOrganizationMembersAsync()
        {
            var response = await _client.Client.GetAsync("members");
            return response;
        }

        public async Task<EmbedContent> ParseHttpResponseToEmbedContentAsync(HttpResponseMessage response)
        {
            var embedContent = new EmbedContent
            {
                Title = "GitHub List Organization Members"
            };

            if (response.IsSuccessStatusCode)
            {
                var descriptionSb = new StringBuilder();
                var json = await response.Content.ReadAsStringAsync();
                var orgMembersJToken = JToken.Parse(json);
                var count = orgMembersJToken.Count();

                descriptionSb.Append($"**Total Members in Organization: {count}**{Environment.NewLine}");

                for (var i = 0; i < count; i++)
                {
                    descriptionSb.Append($"{i + 1}: {orgMembersJToken[i]["login"]} ({orgMembersJToken[i]["url"]}){Environment.NewLine}");
                }
                
                embedContent.Description = descriptionSb.ToString();
            }
            else
            {
                var errMsg =
                    $"There was an error requesting to list the Organization's Members! GitHub's reason was: {response.ReasonPhrase}";

                _logger.Error(errMsg);

                embedContent.Title += ": Failed";
                embedContent.Description = errMsg;
            }

            return embedContent;
        }
    }
}
