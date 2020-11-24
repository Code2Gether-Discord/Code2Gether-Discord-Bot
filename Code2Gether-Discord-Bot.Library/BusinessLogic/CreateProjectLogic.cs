using System;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository;
using Code2Gether_Discord_Bot.Library.Static;
using Discord;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class CreateProjectLogic : IBusinessLogic
    {
        private ILogger _logger;
        private ICommandContext _context;
        private IProjectRepository _projectRepository;
        private string _arguments;

        public CreateProjectLogic(ILogger logger, ICommandContext context, IProjectRepository projectRepository, string arguments)
        {
            _logger = logger;
            _context = context;
            _projectRepository = projectRepository;
            _arguments = arguments;
        }

        public Embed Execute()
        {
            _logger.Log(_context);

            CreateInactiveProject(out string title, out string description);

            return new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle($"Create Project: {title}")
                .WithDescription(description)
                .WithAuthor(_context.User)
                .Build();
        }

        private void CreateInactiveProject(out string title, out string description)
        {
            var projectName = _arguments
                .Trim()
                .Split(' ')[0];

            if (ProjectManager.DoesProjectExist(_projectRepository, projectName))
            {
                title = "Failed";
                description = $"Could not create new inactive project, **{projectName}** already exists!";
            }
            else
            {
                Project newProject = ProjectManager.CreateProject(_projectRepository, projectName, _context.User);
                title = "Success";
                description = $"Successfully created inactive project **{newProject.Name}**!"
                              + Environment.NewLine
                              + Environment.NewLine
                              + $"{_projectRepository.Read(projectName)}";
            }
        }
    }
}