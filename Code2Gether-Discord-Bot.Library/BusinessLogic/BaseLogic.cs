using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Discord;
using Discord.Commands;

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

            _logger.Log(GetType(), _context);
        }

        public abstract Task<Embed> ExecuteAsync();
    }
}
