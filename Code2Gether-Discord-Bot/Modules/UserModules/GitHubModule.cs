using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Static;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Modules.UserModules
{
    public class GitHubModule : ModuleBase<SocketCommandContext>
    {
        #region Lists

        [Command("github list-org",
            RunMode = RunMode.Async)]
        [Alias("list-organization")]
        [Summary("List the GitHub Organization's Members.")]
        public async Task GitHubListOrgAsync([Remainder] string gitHubEmail = "") =>
            await ReplyAsync(embed: await BusinessLogicFactory.GetGitHubListOrgLogic(GetType(), Context, gitHubEmail).ExecuteAsync());

        [Command("github list-teams",
            RunMode = RunMode.Async)]
        [Summary("List the GitHub Organization's Teams.")]
        public async Task GitHubListTeamsAsync([Remainder] string gitHubEmail = "") =>
            await ReplyAsync(embed: await BusinessLogicFactory.GetGitHubListTeamsLogic(GetType(), Context, gitHubEmail).ExecuteAsync());

        #endregion

        #region Joins

        [Command("github join-org",
            RunMode = RunMode.Async)]
        [Alias("join-organization")]
        [Summary("Join the GitHub Organization at https://github.com/Code2Gether-Discord. Usage: github join-org [email")]
        public async Task GitHubJoinOrgAsync([Remainder] string gitHubEmail = "") =>
            await ReplyAsync(embed: await BusinessLogicFactory.GetGitHubJoinOrgLogic(GetType(), Context, gitHubEmail).ExecuteAsync());

        [Command("github join-team",
            RunMode = RunMode.Async)]
        [Summary("Join the a GitHub Team in the Organization. Usage: github join-team [team-slug] [username]")]
        public async Task GitHubJoinTeamAsync([Remainder] string args = "") =>
            await ReplyAsync(embed: await BusinessLogicFactory.GetGitHubJoinTeamLogic(GetType(), Context, args).ExecuteAsync());

        #endregion
    }
}
