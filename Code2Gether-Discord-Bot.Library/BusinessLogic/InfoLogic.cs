using Discord;
using Discord.Commands;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Serilog;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class InfoLogic : BaseLogic
    {
        public InfoLogic(ILogger logger, ICommandContext context) : base(logger, context)
        {
        }

        public override Task<Embed> ExecuteAsync()
        {
            var app = _context.Client.GetApplicationInfoAsync().Result;
            var guilds = _context.Client.GetGuildsAsync().Result;
            
            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle("Info")
                .WithDescription("Plethora of information regarding the current bot process")
                .AddField("Author", $"{app.Owner} ({app.Owner.Id})")
                .AddField("Library", $"Discord.Net ({DiscordConfig.Version})")
                .AddField("Runtime", $"{RuntimeInformation.FrameworkDescription} {RuntimeInformation.ProcessArchitecture} " +
                    $"({RuntimeInformation.OSDescription} {RuntimeInformation.OSArchitecture})")
                .AddField("Uptime", GetUptime())
                .AddField("Heap Size", $"{GetHeapSize()}MiB")
                .AddField("Guilds", $"{guilds.Count}")
                .AddField("Channels", $"{guilds.Sum(g => g.GetChannelsAsync().Result.Count)}")
                .Build();
            return Task.FromResult(embed);
        }

        private static string GetUptime() => (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
        private static string GetHeapSize() => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();
    }
}
