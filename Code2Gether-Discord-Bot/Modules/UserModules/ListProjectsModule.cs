using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Static;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Modules.UserModules
{
    public class ListProjectsModule : ModuleBase<SocketCommandContext>
    {
        [Command("listprojects",
            RunMode = RunMode.Async)]
        [Alias("list")]
        [Summary("Lists all projects that users can join.")]
        public async Task ListProjectsAsync() =>
            await ReplyAsync(embed: await BusinessLogicFactory.GetListProjectsLogic(GetType(), Context).Execute());
    }
}
