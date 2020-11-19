using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Static;
using Discord;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Modules.PrivilegedModules
{
    public class MakeChannelModule : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// Created a new text channel
        /// </summary>
        /// <param name="newChannelName">Argument defining new channel name</param>
        /// <returns>Success or failure embed</returns>
        [Command("makechannel",
            RunMode = RunMode.Async)]
        [Summary("Creates a new text channel")]
        [RequireUserPermission(ChannelPermission.ManageChannels)]
        public async Task MakeChannelAsync([Remainder]string newChannelName = "") =>
            await ReplyAsync(embed: BusinessLogicFactory.MakeChannelLogic(GetType(), Context, newChannelName).Execute());
    }
}
