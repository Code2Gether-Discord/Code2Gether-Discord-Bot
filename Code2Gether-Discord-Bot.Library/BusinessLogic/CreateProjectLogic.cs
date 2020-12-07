using System;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
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

        public async Task<Embed> ExecuteAsync()
        {
            _logger.Log(_context);

            var embedContent = await CreateInactiveProjectAsync();

            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle($"Create Project: {embedContent.Title}")
                .WithDescription(embedContent.Description)
                .WithAuthor(_context.User)
                .Build();
            return embed;
        }

        private async Task<EmbedContent> CreateInactiveProjectAsync()
        {
            var embedContent = new EmbedContent();

            var projectName = ParseCommandArguments.ParseBy(' ', _arguments)[0];

            if (await _projectManager.DoesProjectExistAsync(projectName))
            {
                embedContent.Title = "Failed";
                embedContent.Description = $"Could not create new inactive project, **{projectName}** already exists!";
            }
            else
            {
                // todo: Load user or create new one.
                var user = new Member(_context.User);

                Project newProject = await _projectManager.CreateProjectAsync(projectName, user);
                embedContent.Title = "Success";
                embedContent.Description = $"Successfully created inactive project **{newProject.Name}**!"
                                           + Environment.NewLine
                                           + Environment.NewLine
                                           + $"{newProject}";
            }
            return embedContent;
        }
    }
}