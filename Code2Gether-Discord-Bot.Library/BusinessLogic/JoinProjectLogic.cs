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
            var projectName = _arguments
                .Trim()
                .Split(' ')[0];

            if (ProjectManager.JoinProject(_projectRepository, projectName, _context.User))
            {
                title = "Success";
                description = $"{_context.User} has successfully joined project **{projectName}**!"
                              + Environment.NewLine
                              + Environment.NewLine
                              + $"{_projectRepository.Read(projectName)}";
            }
            else
            {
                title = "Failed";
                description = $"{_context.User} failed to join project **{projectName}**!"
                              + Environment.NewLine
                              + Environment.NewLine
                              + $"{_projectRepository.Read(projectName)}";
            }
        }
    }
}
