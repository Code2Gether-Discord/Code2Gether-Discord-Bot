using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Discord;
using Discord.Commands;
using Serilog;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class MakeChannelLogic : BaseLogic
    {
        private string _newChannelName;

        public MakeChannelLogic(ILogger logger, ICommandContext context, string newChannelName) : base(logger, context)
        {
            _newChannelName = newChannelName;
        }

        public override async Task<Embed> ExecuteAsync()
        {
            var newChannel = await _context.Guild.CreateTextChannelAsync(_newChannelName);

            var embedContent = new EmbedContent();

            if (newChannel is not null)
            {
                embedContent.Title = $"Make Channel: {newChannel.Name}";
                embedContent.Description = $"Successfully made new channel: <#{newChannel.Id}>";
            }
            else
            {
                embedContent.Title= $"Make Channel: {newChannel.Name}";
                embedContent.Description = $"Failed to make new channel: {newChannel.Name}";
            }

            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle(embedContent.Title)
                .WithDescription(embedContent.Description)
                .WithAuthor(_context.Message.Author)
                .Build();
            return embed;
        }
    }
}
