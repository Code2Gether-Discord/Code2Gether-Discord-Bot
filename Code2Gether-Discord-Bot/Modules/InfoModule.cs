using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Modules
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        [Command("info")]
        [Alias("about", "whoami", "owner", "uptime", "library", "author", "stats")]
        public async Task InfoAsync()
        {
            var app = await Context.Client.GetApplicationInfoAsync();
            var embed = new EmbedBuilder()
                .WithColor(58, 2, 153)
                .WithTitle("Info")
                .AddField("Author", $"{app.Owner} ({app.Owner.Id})")
                .AddField("Library", $"Discord.Net ({DiscordConfig.Version})")
                .AddField("Runtime", $"{RuntimeInformation.FrameworkDescription} {RuntimeInformation.ProcessArchitecture} " +
                    $"({RuntimeInformation.OSDescription} {RuntimeInformation.OSArchitecture})")
                .AddField("Uptime", GetUptime())
                .AddField("Heap Size", $"{GetHeapSize()}MiB")
                .AddField("Guilds", $"{Context.Client.Guilds.Count}")
                .AddField("Channels", $"{Context.Client.Guilds.Sum(g => g.Channels.Count)}")
                .AddField("Users", $"{Context.Client.Guilds.Sum(g => g.Users.Count)}")
                .Build();
            await ReplyAsync(embed: embed);
        }

        private static string GetUptime() => (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
        private static string GetHeapSize() => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();
    }
}
