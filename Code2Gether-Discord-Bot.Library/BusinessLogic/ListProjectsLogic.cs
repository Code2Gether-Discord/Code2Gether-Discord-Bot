using System;
using System.Collections.Generic;
using System.Text;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository;
using Discord;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class ListProjectsLogic : IBusinessLogic
    {
        private ILogger _logger;
        private ICommandContext _context;
        private IProjectRepository _projectRepository;

        public ListProjectsLogic(ILogger logger, ICommandContext context, IProjectRepository projectRepository)
        {
            _logger = logger;
            _context = context;
            _projectRepository = projectRepository;
        }

        public Embed Execute()
        {
            _logger.Log(_context);

            ListProjects(out string title, out string description);

            return new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle(title)
                .WithDescription(description)
                .WithAuthor(_context.User)
                .Build();
        }

        private void ListProjects(out string title, out string description)
        {
            int projectCount = 0;

            var sb = new StringBuilder();

            foreach (var project in _projectRepository.ReadAll())
            {
                sb.Append(project.Value
                          + Environment.NewLine
                          + "Current Members: ");
                foreach (var member in project.Value.ProjectMembers)
                {
                    sb.Append($"{member}; ");
                }
                sb.Append(Environment.NewLine);
            }

            title = $"List Projects ({projectCount})";
            description = sb.ToString();
        }
    }
}