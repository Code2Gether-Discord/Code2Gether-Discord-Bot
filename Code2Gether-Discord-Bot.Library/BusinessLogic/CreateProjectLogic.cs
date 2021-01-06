﻿using System;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Static;
using Discord;
using Discord.Commands;
using Serilog;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class CreateProjectLogic : BaseLogic
    {
        private IProjectManager _projectManager;
        private string _arguments;

        public CreateProjectLogic(ILogger logger, ICommandContext context, IProjectManager projectManager, string arguments) : base(logger, context)
        {
            _projectManager = projectManager;
            _arguments = arguments;
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

            var projectName = ParseCommandArguments.ParseBy(' ', _arguments)[0];

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