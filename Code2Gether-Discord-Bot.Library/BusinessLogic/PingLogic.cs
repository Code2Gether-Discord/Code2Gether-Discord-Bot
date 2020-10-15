using Code2Gether_Discord_Bot.Library.Models;
using Discord;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class PingLogic : IBusinessLogic
    {
        ILogger _logger;
        ICommandContext _context;

        int _latency;

        public PingLogic(ILogger logger, ICommandContext context, int latency)
        {
            _logger = logger;
            _context = context;
            _latency = latency;
        }

        public Embed Execute()
        {
            _logger.LogCommandUse(_context);
            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle("Pong!")
                .WithDescription($"Socket Latency: {_latency}ms")
                .WithAuthor(_context.Message.Author)
                .Build();
            return embed;
        }
    }
}
