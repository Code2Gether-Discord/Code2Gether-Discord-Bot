using System;
using System.Collections.Generic;
using System.Text;
using Code2Gether_Discord_Bot.Library.Models;
using Discord;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class MakeChannelLogic : IBusinessLogic
    {
        private ILogger _logger;
        private ICommandContext _context;

        public MakeChannelLogic(ILogger logger, ICommandContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Embed Execute()
        {
            _logger.LogCommandUse(_context);

            if (!_context.Message.Content.Trim().Contains(' ')) throw new ArgumentException("No channel name defined!");

            var newChannel = _context.Message.Content.Split(' ')[1];

            Embed embed;
            if (MakeNewTextChannel(newChannel, out IChannel newChannelObj))
            {
                embed = new EmbedBuilder()
                    .WithColor(Color.Purple)
                    .WithTitle($"Made Channel: {newChannelObj.Name}")
                    .WithDescription($"Successfully made new channel: {newChannel}")
                    .WithAuthor(_context.Message.Author)
                    .Build();
            }
            else
            {
                embed = new EmbedBuilder()
                    .WithColor(Color.Purple)
                    .WithTitle($"Error Making Channel: {newChannelObj.Name}")
                    .WithDescription($"Failed to made new channel: {newChannel}")
                    .WithAuthor(_context.Message.Author)
                    .Build();
            }
            return embed;
        }

        private bool MakeNewTextChannel(string newChannel, out IChannel newChannelObj)
        {
            newChannelObj = _context.Guild.CreateTextChannelAsync(newChannel).Result;
            return newChannel != null;
        }
    }
}
