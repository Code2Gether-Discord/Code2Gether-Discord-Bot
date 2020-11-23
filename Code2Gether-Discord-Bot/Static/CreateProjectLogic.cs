using Code2Gether_Discord_Bot.Library.Models;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Static
{
    internal class CreateProjectLogic : IBusinessLogic
    {
        private ILogger logger;
        private ICommandContext context;

        public CreateProjectLogic(ILogger logger, ICommandContext context)
        {
            this.logger = logger;
            this.context = context;
        }
    }
}