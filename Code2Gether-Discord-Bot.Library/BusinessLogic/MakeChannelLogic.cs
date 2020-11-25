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

        public Task<Embed> ExecuteAsync()
        {
            _logger.Log(_context);

            if (MakeNewTextChannel(_newChannelName, out IChannel newChannelObj))
            {
                var embed = new EmbedBuilder()
                    .WithColor(Color.Purple)
                    .WithTitle($"Made Channel: {newChannelObj.Name}")
                    .WithDescription($"Successfully made new channel: <#{newChannelObj.Id}>")
                    .WithAuthor(_context.Message.Author)
                    .Build();
                return Task.FromResult(embed);
            }

            throw new Exception($"Failed to create channel: {_newChannelName}");
        }

        private bool MakeNewTextChannel(string newChannel, out IChannel newChannelObj)
        {
            newChannelObj = _context.Guild.CreateTextChannelAsync(newChannel).Result;
            return newChannel != null;
        }
    }
}
