using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Serilog;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public abstract class BaseLogic : IBusinessLogic
    {
        internal ILogger _logger;
        internal ICommandContext _context;

        public BaseLogic(ILogger logger, ICommandContext context)
        {
            _logger = logger;
            _context = context;

            _logger.Information($"{GetType()} executed by {context.Message.Author} on {context.Guild} #{context.Channel}: {context.Message}");
        }

        public abstract Task<Embed> ExecuteAsync();
    }
}
