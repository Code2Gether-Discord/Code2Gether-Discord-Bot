using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Discord;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class MakeChannelLogic : IBusinessLogic
    {
        private ILogger _logger;
        private ICommandContext _context;
        private string _newChannelName;

        public MakeChannelLogic(ILogger logger, ICommandContext context, string newChannelName)
        {
            _logger = logger;
            _context = context;
            _newChannelName = newChannelName;
        }

        public async Task<Embed> ExecuteAsync()
        {
            _logger.Log(_context);

            var newChannel = await _context.Guild.CreateTextChannelAsync(_newChannelName);

            if (newChannel is not null)
            {
                var embed = new EmbedBuilder()
                    .WithColor(Color.Purple)
                    .WithTitle($"Made Channel: {newChannel.Name}")
                    .WithDescription($"Successfully made new channel: <#{newChannel.Id}>")
                    .WithAuthor(_context.Message.Author)
                    .Build();
                return embed;
            }

            throw new Exception($"Failed to create channel: {_newChannelName}");
        }
    }
}
