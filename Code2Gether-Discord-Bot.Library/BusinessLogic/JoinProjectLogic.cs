using System;
using System.Linq;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
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

        public async Task<Embed> ExecuteAsync()
        {
            _logger.Log(_context);

            var embedContent = await JoinProjectAsync();

            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle($"Join Project: {embedContent.Title}")
                .WithDescription(embedContent.Description)
                .WithAuthor(_context.User)
                .Build();

            return embed;
        }

        private async Task<EmbedContent> JoinProjectAsync()
        {
            var embedContent = new EmbedContent();
            var projectName = ParseCommandArguments.ParseBy(' ', _arguments)[0];

            var user = new Member(_context.User);    
            var result = await _projectManager.JoinProjectAsync(projectName, user);
            var project = await _projectManager.GetProjectAsync(projectName);
            
            if (result)
            {
                embedContent.Title = "Success";
                embedContent.Description = $"{_context.User} has successfully joined project **{projectName}**!"
                                         + Environment.NewLine
                                         + Environment.NewLine
                                         + $"{project}";

                if (project.IsActive) // If project has become active from new user
                    TransitionToActiveProject(project);
            }
            else
            {
                embedContent.Title = "Failed";
                embedContent.Description = $"{_context.User} failed to join project **{projectName}**!"
                                           + Environment.NewLine
                                           + Environment.NewLine
                                           + $"{project}";
            }

            return embedContent;
        }

        private async void TransitionToActiveProject(Project project)
        {
            // Find a category in the guild called "PROJECTS"
            ulong? projCategoryId = _context.Guild
                .GetCategoriesAsync().Result
                .FirstOrDefault(c => c.Name
                    .Contains("PROJECTS"))?.Id;

            // Create new text channel under that category
            var channel = await _context.Guild.CreateTextChannelAsync(project.Name, p =>
            {
                if (projCategoryId != null)
                    p.CategoryId = projCategoryId;
            });

            // Create new role
            var role = await _context.Guild
                .CreateRoleAsync($"project-{project.Name}", GuildPermissions.None, null, false, true);

            
            // Give every project member the role
            foreach (var member in project.Members)
            {
                await _context.Guild
                    .GetUserAsync(member.DiscordUserInfo.Id).Result
                    .AddRoleAsync(role);
            }
            
            // Notify members in new channel
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
