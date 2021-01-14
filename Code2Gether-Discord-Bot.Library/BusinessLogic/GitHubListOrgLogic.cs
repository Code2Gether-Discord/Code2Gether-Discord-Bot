﻿using System.Net.Http;
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
            var result = ParseHttpResponseToEmbedContent(await GetOrganizationMembersAsync());

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

        public EmbedContent ParseHttpResponseToEmbedContent(HttpResponseMessage response)
        {
            var embedContent = new EmbedContent
            {
                Title = "GitHub List Organization Members: "
            };

            if (response.IsSuccessStatusCode)
            {
                var descriptionSb = new StringBuilder();
                var orgMembersJObject = new JObject(response.Content);

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
