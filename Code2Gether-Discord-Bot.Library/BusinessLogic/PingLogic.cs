using System;
using Code2Gether_Discord_Bot.Library.Models;
using Discord;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class PingLogic : IBusinessLogic
    {
        ILogger _logger;
        ICommandContext _context;
        int _latency;   // Latency must be passed in since it's a client function

        public PingLogic(ILogger logger, ICommandContext context, int latency)
        {
            _logger = logger;
            _context = context;
            _latency = latency;
        }

        public Embed Execute()
        {
            _logger.LogCommandUse(_context);
            string socLatencyString = $"Socket Latency: {_latency}ms";
            Embed embed;
            try
            {
                var m = _context.Channel.SendMessageAsync("https://tenor.com/view/loading-buffering-gif-8820437").Result;

                embed = new EmbedBuilder()
                    .WithColor(Color.Purple)
                    .WithTitle("Pong!")
                    .WithDescription(socLatencyString + Environment.NewLine +
                                     $"Message Latency {m.CreatedAt.UtcDateTime.Subtract(_context.Message.CreatedAt.UtcDateTime).Milliseconds}ms")
                    .WithAuthor(_context.Message.Author)
                    .Build();

                m.DeleteAsync();
            }
            catch (Exception e)
            {
                _logger.Log(LogSeverity.Error, $"Failed to ping with Message Latency: {e.Message}");
                embed = new EmbedBuilder()
                    .WithColor(Color.Purple)
                    .WithTitle("Pong!")
                    .WithDescription(socLatencyString)
                    .WithAuthor(_context.Message.Author)
                    .Build();
            }

            return embed;
        }
    }
}
