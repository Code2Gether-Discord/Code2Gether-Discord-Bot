using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Static;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Modules.UserModules
{
    public class GitHubModule : ModuleBase<SocketCommandContext>
    {
        [Command("github join",
            RunMode = RunMode.Async)]
        [Summary("Join the GitHub Organization at https://github.com/Code2Gether-Discord with your GitHub Account's Email")]
        public async Task GitHubJoinAsync([Remainder] string gitHubEmail = "") =>
            await ReplyAsync(embed: await BusinessLogicFactory.GetGitHubJoinLogic(GetType(), Context, gitHubEmail).ExecuteAsync());
    }
}
