using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using GitHubApiWrapper;
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

        public override Task<Embed> ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
