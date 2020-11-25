using System;
using System.Linq;
using System.Threading.Tasks;
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

        public Task<Embed> ExecuteAsync()
        {
            _logger.Log(_context);

            string socLatencyString = $"Socket Latency: {_latency}ms";
            Embed embed;

            try
            {
                if (!_context.Guild.GetCurrentUserAsync().Result.GuildPermissions.ManageMessages)
                {
                    throw new Exception("Missing ManageMessages permission for Message Latency");
                }

                var temporaryMessage = _context.Channel.SendMessageAsync("https://tenor.com/view/loading-buffering-gif-8820437").Result;

                embed = new EmbedBuilder()
                    .WithColor(Color.Purple)
                    .WithTitle("Pong!")
                    .WithDescription(socLatencyString + Environment.NewLine +
                                     $"Message Latency {temporaryMessage.CreatedAt.UtcDateTime.Subtract(_context.Message.CreatedAt.UtcDateTime).Milliseconds}ms")
                    .WithAuthor(_context.Message.Author)
                    .Build();

                temporaryMessage.DeleteAsync();
            }
            catch (Exception e)
            {
                _logger.Log(LogSeverity.Error, e);

                embed = new EmbedBuilder()
                    .WithColor(Color.Purple)
                    .WithTitle("Pong!")
                    .WithDescription(socLatencyString)
                    .WithAuthor(_context.Message.Author)
                    .Build();
            }

            return Task.FromResult(embed);
        }
    }
}
