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

        public override Task<Embed> ExecuteAsync()
        {
            if (!MakeNewTextChannel(_newChannelName, out IChannel newChannelObj))
                throw new Exception($"Failed to create channel: {_newChannelName}");

            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle($"Made Channel: {newChannelObj.Name}")
                .WithDescription($"Successfully made new channel: <#{newChannelObj.Id}>")
                .WithAuthor(_context.Message.Author)
                .Build();
            return Task.FromResult(embed);

        }

        private bool MakeNewTextChannel(string newChannel, out IChannel newChannelObj)
        {
            newChannelObj = _context.Guild.CreateTextChannelAsync(newChannel).Result;
            return newChannel != null;
        }
    }
}
