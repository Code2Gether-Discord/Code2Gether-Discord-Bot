﻿using Code2Gether_Discord_Bot.Library.Models;
using Discord;
using Discord.Commands;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class InfoLogic : IBusinessLogic
    {
        ILogger _logger;
        ICommandContext _context;

        public InfoLogic(ILogger logger, ICommandContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Make this return Task<Embed> (even though it will execute synchronously)
        public Embed Execute()
        {
            _logger.Log(_context);

            var app = _context.Client.GetApplicationInfoAsync().Result;
            var guilds = _context.Client.GetGuildsAsync().Result;
            
            return new EmbedBuilder()
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

            // Return Task.FromResult
        }

        private static string GetUptime() => (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
        private static string GetHeapSize() => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();
    }
}
