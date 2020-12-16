using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Discord;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class LeetLogic : IBusinessLogic
    {
        private ILogger _logger;
        private ICommandContext _context;
        private string _args;

        public LeetLogic(ILogger logger, ICommandContext context, string args)
        {
            _logger = logger;
            _context = context;
            _args = args;
        }

        public Task<Embed> ExecuteAsync()
        {
            _logger.Log(_context);

            // Set from a private method
            string title = string.Empty;    // Title for leet question
            string description = string.Empty;  // Prompt for leet question

            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle($"Leet Question: {title}")
                .WithDescription(description)
                .WithAuthor(_context.Message.Author)
                .Build();

            return Task.FromResult(embed);
        }
    }
}
