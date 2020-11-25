using System;
using System.Threading.Tasks;
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
        private IProjectManager _projectManager;
        private string _arguments;

        public CreateProjectLogic(ILogger logger, ICommandContext context, IProjectManager projectManager, string arguments)
        {
            _logger = logger;
            _context = context;
            _projectManager = projectManager;
            _arguments = arguments;
        }

        public Task<Embed> Execute()
        {
            _logger.Log(_context);

            CreateInactiveProject(out string title, out string description);

            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle($"Create Project: {title}")
                .WithDescription(description)
                .WithAuthor(_context.User)
                .Build();
            return Task.FromResult(embed);
        }

        private void CreateInactiveProject(out string title, out string description)
        {
            var projectName = _arguments
                .Trim()
                .Split(' ')[0];

            if (_projectManager.DoesProjectExist(projectName))
            {
                title = "Failed";
                description = $"Could not create new inactive project, **{projectName}** already exists!";
            }
            else
            {
                Project newProject = _projectManager.CreateProject(projectName, _context.User);
                title = "Success";
                description = $"Successfully created inactive project **{newProject.Name}**!"
                              + Environment.NewLine
                              + Environment.NewLine
                              + $"{newProject}";
            }
        }
    }
}