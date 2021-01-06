using Code2Gether_Discord_Bot.Library.Models;
using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class HelpLogic : BaseLogic
    {
        private IEnumerable<ModuleInfo> _modules;
        private string _prefix;

        public HelpLogic(ILogger logger, ICommandContext context, IEnumerable<ModuleInfo> modules, string prefix) : base(logger, context)
        {
            _modules = modules;
            _prefix = prefix;
        }

        public override Task<Embed> ExecuteAsync()
        {
            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle("Help")
                .WithDescription($"Contains information on how to access this bot's command modules")
                .AddField("Commands", GetCommandText())
                .WithAuthor(_context.Message.Author)
                .Build();

            return Task.FromResult(embed);
        }

        private string GetCommandText()
        {
            var commandTextSb = new StringBuilder();
            foreach (var module in _modules)
            {
                foreach (var command in module.Commands)
                {
                    var aliasesSb = new StringBuilder();
                    foreach (var alias in command.Aliases)
                    {
                        aliasesSb.Append($"{alias};");
                    }
                    commandTextSb.Append($"{_prefix}{command.Name} ({aliasesSb}) - {command.Summary}{Environment.NewLine}");
                }
            }
            return commandTextSb.ToString();
        }
    }
}