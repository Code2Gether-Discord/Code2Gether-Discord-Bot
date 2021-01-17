using System;
using Code2Gether_Discord_Bot.Static;
using Discord.Commands;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Modules.UserModules
{
    [Obsolete("Deactivated until further notice")]
    class CreateProjectModule : ModuleBase<SocketCommandContext>
    {
        [Obsolete("Deactivated until further notice")]
        [Command("createproject",
            RunMode = RunMode.Async)]
        [Alias("create")]
        [Summary("Creates an inactive project that other users can join.")]
        private async Task CreateProjectAsync([Remainder] string projectName) =>
             await ReplyAsync(embed: await BusinessLogicFactory.GetCreateProjectLogic(GetType(), Context, projectName).ExecuteAsync());
    }
}
