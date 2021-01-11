using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Static;
using Discord;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Modules.UserModules
{
    public class JoinProjectModule : ModuleBase<SocketCommandContext>
    {
        [Command("joinproject",
            RunMode = RunMode.Async)]
        [Alias("join")]
        [Summary("Joins a project.")]
        [RequireBotPermission(ChannelPermission.ManageChannels, ErrorMessage = "Missing permission to Manage Channels")]
        [RequireBotPermission(ChannelPermission.ManageRoles, ErrorMessage = "Missing permission to Manage Roles")]
        public async Task JoinProjectAsync([Remainder] string projectName) =>
            await ReplyAsync(embed: await BusinessLogicFactory.GetJoinProjectLogic(GetType(), Context, projectName).ExecuteAsync());
    }
}
