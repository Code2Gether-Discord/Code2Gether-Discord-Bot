using System;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Static;
using Discord;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class CreateProjectLogic : BaseLogic
    {
        private IProjectManager _projectManager;
        private string _projectName;

        public CreateProjectLogic(ILogger logger, ICommandContext context, IProjectManager projectManager, string projectName) : base(logger, context)
        {
            _projectManager = projectManager;
            _projectName = projectName;
        }

        public override async Task<Embed> ExecuteAsync()
        {
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

            var projectName = ParseCommandArguments.ParseBy(' ', _projectName)[0];

            // Check if a project exists before creating one (unique project names)
            if (await _projectManager.DoesProjectExistAsync(projectName))
            {
                embedContent.Title = "Failed";
                embedContent.Description = $"Could not create new inactive project, **{projectName}** already exists!";
            }

            // If no project exists by that name
            else
            {
                var user = new Member(_context.User);

                // Create a new project
                Project newProject = await _projectManager.CreateProjectAsync(projectName, user);

                embedContent.Title = "Success";
                embedContent.Description = $"Successfully created inactive project **{newProject.Name}**!"
                                           + Environment.NewLine
                                           + Environment.NewLine
                                           + newProject;
            }

            return embedContent;
        }
    }
}