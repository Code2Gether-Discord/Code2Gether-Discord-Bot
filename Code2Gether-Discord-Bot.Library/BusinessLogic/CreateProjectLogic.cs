using Code2Gether_Discord_Bot.Library.Models;
using Discord;
using Discord.Commands;
using System;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    internal class CreateProjectLogic : IBusinessLogic
    {
        private ILogger _logger;
        private ICommandContext _context;
        private string _arguments;

        public CreateProjectLogic(ILogger logger, ICommandContext context, string arguments)
        {
            _logger = logger;
            _context = context;
            _arguments = arguments;
        }

        public Embed Execute()
        {
            _logger.Log(_context);

            CreateInactiveProject(out string title, out string description);

            return new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle(title)
                .WithDescription(description)
                .WithAuthor(_context.User)
                .Build();
        }

        private void CreateInactiveProject(out string title, out string description)
        {
            bool result = false;

            title = $"Created project: {projName}";
            description = $"Inactive project creat";
            description += result ? "ed successfully!" : "ion failed!";
        }
    }
}