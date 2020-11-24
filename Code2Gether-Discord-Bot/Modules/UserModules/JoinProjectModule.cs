using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Static;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Modules.UserModules
{
    public class JoinProjectModule : ModuleBase<SocketCommandContext>
    {
        [Command("joinproject",
            RunMode = RunMode.Async)]
        [Alias("join")]
        [Summary("Joins a project.")]
        public async Task JoinProjectAsync([Remainder] string arguments) =>
            await ReplyAsync(embed: BusinessLogicFactory.GetJoinProjectLogic(GetType(), Context, arguments).Execute());
    }
}
