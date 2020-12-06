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
            var projects = await _projectRepository.ReadAllAsync();

            foreach (var project in projects)
            {
                sb.Append(project
                          + Environment.NewLine
                          + "Current Members: ");

                foreach (var member in project.Members)
                {
                    sb.Append($"{member}; ");
                }

                sb.Append(Environment.NewLine);
            }

            embedContent.Title = $"List Projects ({projects.Count()})";
            embedContent.Description = sb.ToString();
            return embedContent;
        }
    }
}