using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Static;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Modules.UserModules
{
    public class PingModule : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// Replies with an embed containing the client's websocket latency
        /// </summary>
        /// <returns></returns>
        [Command("ping",
            RunMode = RunMode.Async)]
        [Alias("pong")]
        [Summary("Replies with an embed containing the bot's websocket latency")]
        public async Task PingAsync() =>
            await ReplyAsync(embed: BusinessLogicFactory.PingLogic(GetType(),Context, Context.Client.Latency).Execute());
    }
}
