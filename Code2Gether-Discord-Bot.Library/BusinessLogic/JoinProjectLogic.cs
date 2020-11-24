using System;
using System.Collections.Generic;
using System.Text;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository;
using Discord;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class JoinProjectLogic : IBusinessLogic
    {
        private ILogger _logger;
        private ICommandContext _context;
        private IProjectRepository _projectRepository;
        private string _arguments;

        public JoinProjectLogic(ILogger logger, ICommandContext context, IProjectRepository projectRepository, string arguments)
        {
            _logger = logger;
            _context = context;
            _projectRepository = projectRepository;
            _arguments = arguments;
        }

        public Embed Execute()
        {
            _logger.Log(_context);

            JoinProject(out string title, out string description);

            return new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle($"Join Project: {title}")
                .WithDescription(description)
                .WithAuthor(_context.User)
                .Build();
        }

        private void JoinProject(out string title, out string description)
        {
            throw new NotImplementedException();
        }
    }
}
