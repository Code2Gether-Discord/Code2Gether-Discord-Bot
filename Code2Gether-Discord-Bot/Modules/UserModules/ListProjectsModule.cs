using System;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Static;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Modules.UserModules
{
    [Obsolete("Deactivated until further notice")]
    class ListProjectsModule : ModuleBase<SocketCommandContext>
    {
        [Command("listprojects",
            RunMode = RunMode.Async)]
        [Alias("list")]
        [Summary("Lists all projects that users can join.")]
        private async Task ListProjectsAsync() =>
            await ReplyAsync(embed: await BusinessLogicFactory.GetListProjectsLogic(GetType(), Context).ExecuteAsync());
    }
}
