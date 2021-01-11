using System;
using System.Linq;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Static;
using Discord;
using Discord.Commands;
using Serilog;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class JoinProjectLogic : BaseLogic
    {
        private IProjectManager _projectManager;
        private string _projectName;

        public JoinProjectLogic(ILogger logger, ICommandContext context, IProjectManager projectManager, string projectName) : base(logger, context)
        {
            _projectManager = projectManager;
            _projectName = projectName;
        }

        public override async Task<Embed> ExecuteAsync()
        {
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

            var projectName = ParseCommandArguments.ParseBy(' ', _projectName)[0];

            var user = new Member(_context.User);   
            
            // Attempt to join the project
            var result = await _projectManager.JoinProjectAsync(projectName, user);

            // Get the updated project object
            var project = await _projectManager.GetProjectAsync(projectName);
            
            // If joining was successful
            if (result)
            {
                embedContent.Title = "Success";
                embedContent.Description = $"{_context.User} has successfully joined project **{projectName}**!"
                                         + Environment.NewLine
                                         + Environment.NewLine
                                         + project;

                // If project has become active from new user
                if (project.IsActive) 
                    TransitionToActiveProject(project);
            }
            else
            {
                embedContent.Title = "Failed";
                embedContent.Description = $"{_context.User} failed to join project **{projectName}**!"
                                           + Environment.NewLine
                                           + Environment.NewLine
                                           + project;
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
            bool channelAlreadyCreated = false;
            var channels = await _context.Guild.GetChannelsAsync();
            ITextChannel channel = null;
            if (channels.Count(c => c.Name.Contains(project.Name)) == 0)
            {
                channel = await _context.Guild.CreateTextChannelAsync(project.Name, p =>
                {
                    if (projCategoryId != null)
                        p.CategoryId = projCategoryId;
                });
            }
            else
            {
                channelAlreadyCreated = true;
            }

            // Create new role
            var roleName = $"project-{project.Name}";
            var roles = _context.Guild.Roles;
            IRole role;
            if (roles.Count(r => r.Name.Contains(roleName)) == 0)
            {
                role = await _context.Guild
                    .CreateRoleAsync(roleName, GuildPermissions.None, null, false, true);
            }
            else
            {
                role = _context.Guild.Roles.FirstOrDefault(r => r.Name.Contains(roleName));
            }

            // Give every project member the role
            foreach (var member in project.Members)
            {
                // todo: populate DiscordUserId based on the snowflake ID here.
                // Causes a null refernece exception if it doesn't.

                await _context.Guild
                    .GetUserAsync(member.SnowflakeId).Result
                    .AddRoleAsync(role);
            }

            if (!channelAlreadyCreated)
            {
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
}
