using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Modules
{
    class PingModule : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Alias("pong")]
        [Summary("Replies with Socket Latency")]
        public async Task PingAsync()
        {
            var embed = new EmbedBuilder()
                .WithTitle("Pong!")
                .WithDescription($"Socket Latency: {Context.Client.Latency}ms")
                .Build();
            await ReplyAsync(embed: embed);
        }
    }
}
