using Code2Gether_Discord_Bot.Library.Models;
using Discord;
using Discord.Commands;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Code2Gether_Discord_Bot.Static
{
    public class HelpLogic : IBusinessLogic
    {
        private ILogger _logger;
        private ICommandContext _context;
        private IEnumerable<ModuleInfo> _modules;
        private string _prefix;

        public HelpLogic(ILogger logger, ICommandContext context, IEnumerable<ModuleInfo> modules, string prefix)
        {
            _logger = logger;
            _context = context;
            _modules = modules;
            _prefix = prefix;
        }

        public Embed Execute()
        {
            _logger.LogCommandUse(_context, GetType());
            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle("Help")
                .WithDescription($"Contains information on how to access this bot's command modules")
                .AddField("Commands", GetCommandText())
                .WithAuthor(_context.Message.Author)
                .Build();
            return embed;
        }

        private string GetCommandText()
        {
            string commandText = "";
            foreach (var module in _modules)
            {
                foreach (var command in module.Commands)
                {
                    string aliases = "";
                    foreach (var alias in command.Aliases)
                    {
                        aliases += $"{alias};";
                    }
                    commandText += $"{_prefix}{command.Name} ({aliases}) - {command.Summary}{Environment.NewLine}";
                }
            }
            return commandText;
        }
    }
}