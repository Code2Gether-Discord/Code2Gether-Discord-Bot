using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Static;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Modules.UserModules
{
    public class GitHubModule : ModuleBase<SocketCommandContext>
    {
        [Command("github join-org",
            RunMode = RunMode.Async)]
        [Alias("join-organization")]
        [Summary("Join the GitHub Organization at https://github.com/Code2Gether-Discord with your GitHub Account's Email")]
        public async Task GitHubJoinAsync([Remainder] string gitHubEmail = "") =>
            await ReplyAsync(embed: await BusinessLogicFactory.GetGitHubJoinLogic(GetType(), Context, gitHubEmail).ExecuteAsync());

        [Command("github join-team",
            RunMode = RunMode.Async)]
        [Summary("Join the a GitHub Team in the Organization at https://github.com/Code2Gether-Discord. Usage: github join team [team name] [username]")]
        public async Task GitHubJoinTeamAsync([Remainder] string args = "") =>
            await ReplyAsync(embed: await BusinessLogicFactory.GetGitHubJoinTeamLogic(GetType(), Context, args).ExecuteAsync());
    }
}
