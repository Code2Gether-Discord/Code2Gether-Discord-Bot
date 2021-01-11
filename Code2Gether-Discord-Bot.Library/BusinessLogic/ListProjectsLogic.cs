using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.Repositories;
using Discord;
using Discord.Commands;
using Serilog;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class ListProjectsLogic : BaseLogic
    {
        private IProjectRepository _projectRepository;

        public ListProjectsLogic(ILogger logger, ICommandContext context, IProjectRepository projectRepository) : base(logger, context)
        {
            _projectRepository = projectRepository;
        }

        public override async Task<Embed> ExecuteAsync()
        {
            var embedContent = await ListProjectsAsync();

            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle(embedContent.Title)
                .WithDescription(embedContent.Description)
                .WithAuthor(_context.User)
                .Build();

            return embed;
        }

        private async Task<EmbedContent> ListProjectsAsync()
        {
            var embedContent = new EmbedContent();

            var sb = new StringBuilder();

            // Read all projects
            var projects = new List<Project>();
            projects.AddRange(await _projectRepository.ReadAllAsync());

            for (var i = 0; i < projects.Count; i++)
            {
                sb.Append($"[{i+1}/{projects.Count}]" + projects[i]
                          + Environment.NewLine
                          + Environment.NewLine);
            }

            embedContent.Title = $"List Projects ({projects.Count})"; // "List Projects (0)"
            embedContent.Description = sb.ToString();   // "some-project ; Created by: SomeUser#1234 \r\n Current Members: SomeUser#1234 \r\n "
            return embedContent;
        }
    }
}