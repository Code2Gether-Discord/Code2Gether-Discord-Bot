using System;
using System.Collections.Generic;
using System.Text;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository;
using Code2Gether_Discord_Bot.Library.Static;
using Discord;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class JoinProjectLogic : IBusinessLogic
    {
        private ILogger _logger;
        private ICommandContext _context;
        private IProjectManager _projectManager;
        private string _arguments;

        public JoinProjectLogic(ILogger logger, ICommandContext context, IProjectManager projectManager, string arguments)
        {
            _logger = logger;
            _context = context;
            _projectManager = projectManager;
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
            var projectName = _arguments
                .Trim()
                .Split(' ')[0];

            if (_projectManager.JoinProject(projectName, _context.User, out Project project))
            {
                title = "Success";
                description = $"{_context.User} has successfully joined project **{projectName}**!"
                              + Environment.NewLine
                              + Environment.NewLine
                              + $"{project}";
            }
            else
            {
                title = "Failed";
                description = $"{_context.User} failed to join project **{projectName}**!"
                              + Environment.NewLine
                              + Environment.NewLine
                              + $"{project}";
            }
        }
    }
}
