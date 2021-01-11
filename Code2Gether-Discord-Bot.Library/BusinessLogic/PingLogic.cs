using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Serilog;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class PingLogic : BaseLogic
    {
        private readonly int _latency;   // Latency must be passed in since it's a client function

        public PingLogic(ILogger logger, ICommandContext context, int latency) : base(logger, context)
        {
            _latency = latency;
        }

        public override async Task<Embed> ExecuteAsync()
        {
            var socLatencyString = $"Socket Latency: {_latency}ms";
            Embed embed;

            try
            {
                var selfUser = await _context.Guild.GetCurrentUserAsync();
                if (!selfUser.GuildPermissions.ManageMessages)
                {
                    throw new Exception("Missing ManageMessages permission for Message Latency");
                }

                var temporaryMessage = await _context.Channel.SendMessageAsync("https://tenor.com/view/loading-buffering-gif-8820437");

                embed = new EmbedBuilder()
                    .WithColor(Color.Purple)
                    .WithTitle("Pong!")
                    .WithDescription(socLatencyString + Environment.NewLine +
                                     $"Message Latency {temporaryMessage.CreatedAt.UtcDateTime.Subtract(_context.Message.CreatedAt.UtcDateTime).Milliseconds}ms")
                    .WithAuthor(_context.Message.Author)
                    .Build();

                await temporaryMessage.DeleteAsync();
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to use preferred method for pinging!");

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
