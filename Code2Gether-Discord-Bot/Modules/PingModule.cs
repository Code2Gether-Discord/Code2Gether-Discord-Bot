using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Modules
{
    public class PingModule : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Alias("pong")]
        public async Task PingAsync()
        {
            var embed = new EmbedBuilder()
                .WithColor(58,2,153)
                .WithTitle("Pong!")
                .WithDescription($"Socket Latency: {Context.Client.Latency}ms")
                .WithAuthor(Context.Message.Author)
                .Build();
            await ReplyAsync(embed: embed);
        }
    }
}
