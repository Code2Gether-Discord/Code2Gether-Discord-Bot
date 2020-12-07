using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.Repositories;
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
        
        public async Task<Embed> ExecuteAsync()
        {
            _logger.Log(_context);

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
            var projects = await _projectRepository.ReadAllAsync();

            foreach (var project in projects)
            {

                // Get Discord User details for project's author
                var authorUser = await _context.Guild.GetUserAsync(project.Author.SnowflakeId);

                sb.Append($"{project.Name}; Created by: {authorUser.Username}#{authorUser.Discriminator}"
                          + Environment.NewLine
                          + "Current Members: ");

                foreach (var member in project.Members)
                {
                    // Get the Discord user for each project's member
                    var user = await _context.Guild.GetUserAsync(member.SnowflakeId);
                    sb.Append($"{user}; ");
                }

                sb.Append(Environment.NewLine);
            }

            embedContent.Title = $"List Projects ({projects.Count()})"; // "List Projects (0)"
            embedContent.Description = sb.ToString();   // "some-project ; Created by: SomeUser#1234 \r\n Current Members: SomeUser#1234 \r\n "
            return embedContent;
        }
    }
}