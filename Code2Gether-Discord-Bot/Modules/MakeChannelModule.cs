using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Static;
using Discord;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Modules
{
    class MakeChannelModule : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// Created a new text channel
        /// </summary>
        /// <returns>Success or failure embed</returns>
        [Command("makechannel",
            RunMode = RunMode.Async)]
        [Summary("Creates a new text channel")]
        [RequireUserPermission(ChannelPermission.ManageChannels)]
        public async Task MakeChannelAsync() =>
            await ReplyAsync(embed: BusinessLogicFactory.MakeChannelLogic(GetType(), Context).Execute());
    }
}
