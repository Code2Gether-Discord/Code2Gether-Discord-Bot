using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Discord;
using Discord.Commands;
namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class LeetAnsLogic : BaseLogic
    {
        private string _args;

        public LeetAnsLogic(ILogger logger, ICommandContext context, string args) : base(logger, context)
        {
            _args = args;
        }

        public override Task<Embed> ExecuteAsync()
        {
            // Set from a private method
            string title = "Coming Soon";    // Title for leet answer
            string description = "This command is still under development.";  // Leet answer response

            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle($"Leet Answer: {title}")
                .WithDescription(description)
                .WithAuthor(_context.Message.Author)
                .Build();

            return Task.FromResult(embed);
        }

    }
}