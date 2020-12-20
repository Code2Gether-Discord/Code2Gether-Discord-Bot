using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Discord;
using Discord.Commands;

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

            var title = string.Empty;
            var description = string.Empty;

            if (newChannel is not null)
            {
                title = $"Make Channel: {newChannel.Name}";
                description = $"Successfully made new channel: <#{newChannel.Id}>";
            }
            else
            {
                title = $"Make Channel: {newChannel.Name}";
                description = $"Failed to make new channel: {newChannel.Name}";
            }

            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle(title)
                .WithDescription(description)
                .WithAuthor(_context.Message.Author)
                .Build();
            return embed;
        }
    }
}
