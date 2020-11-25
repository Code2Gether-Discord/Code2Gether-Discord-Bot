using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private IProjectManager _projectManager;
        private string _arguments;

        public JoinProjectLogic(ILogger logger, ICommandContext context, IProjectManager projectManager, string arguments)
        {
            _logger = logger;
            _context = context;
            _projectManager = projectManager;
            _arguments = arguments;
        }

        public Task<Embed> Execute()
        {
            _logger.Log(_context);

            JoinProject(out string title, out string description);

            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle($"Join Project: {title}")
                .WithDescription(description)
                .WithAuthor(_context.User)
                .Build();
            return Task.FromResult(embed);
        }

        private void JoinProject(out string title, out string description)
        {
            var projectName = _arguments
                .Trim()
                .Split(' ')[0];

            _ = _projectManager.DoesProjectExist(projectName, out Project tProject);

            if (_projectManager.JoinProject(projectName, _context.User, out Project project))
            {
                title = "Success";
                description = $"{_context.User} has successfully joined project **{projectName}**!"
                              + Environment.NewLine
                              + Environment.NewLine
                              + $"{project}";

                if (!tProject.IsActive && project.IsActive) // If project has become active from new user
                    TransitionToActiveProject(project);
            }
            else
            {
                title = "Failed";
                description = $"{_context.User} failed to join project **{projectName}**!"
                              + Environment.NewLine
                              + Environment.NewLine
                              + $"{project}";
            }
        }

        private async void TransitionToActiveProject(Project project)
        {
            ulong? projCategoryId = _context.Guild
                .GetCategoriesAsync().Result
                .FirstOrDefault(c => c.Name
                    .Contains("PROJECTS"))?.Id;

            var channel = await _context.Guild.CreateTextChannelAsync(project.Name, p =>
            {
                if (projCategoryId != null)
                    p.CategoryId = projCategoryId;
            });

            var role = await _context.Guild
                .CreateRoleAsync($"project-{project.Name}", GuildPermissions.None, null, false, true);

            foreach (var member in project.ProjectMembers)
            {
                await _context.Guild
                    .GetUserAsync(member.Id).Result
                    .AddRoleAsync(role);
            }

            await channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle("New Active Project")
                .WithDescription($"A new project has gained enough members to become active!"
                    + Environment.NewLine
                    + project)
                .Build());
        }
    }
}
