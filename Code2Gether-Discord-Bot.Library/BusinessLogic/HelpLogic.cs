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
        private readonly IEnumerable<ModuleInfo> _modules;
        private readonly string _prefix;

        public HelpLogic(ILogger logger, ICommandContext context, IEnumerable<ModuleInfo> modules, string prefix) : base(logger, context)
        {
            _modules = modules;
            _prefix = prefix;
        }

        public override Task<Embed> ExecuteAsync()
        {
            var commandText = GetCommandText();

            var embedBuilder = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle("Help")
                .WithDescription("Contains information on how to access this bot's command modules")
                .WithAuthor(_context.Message.Author);

            for (var i = 0; i < commandText.Count; i++)
            {
                embedBuilder.AddField($"Command {i + 1} of {commandText.Count}", commandText[i]);
            }

            var embed = embedBuilder.Build();

            return Task.FromResult(embed);
        }

        private List<string> GetCommandText()
        {
            var commandTexts = new List<string>();

            foreach (var module in _modules)
            {
                var moduleStringBuilder = new StringBuilder();

                foreach (var command in module.Commands)
                {
                    var commandAliasStringBuilder = new StringBuilder();

                    foreach (var alias in command.Aliases)
                    {
                        commandAliasStringBuilder.Append($"{alias}; ");
                    }

                    moduleStringBuilder.Append($"{_prefix}{command.Name} ({commandAliasStringBuilder}) - {command.Summary}{Environment.NewLine}");
                }

                commandTexts.Add(moduleStringBuilder.ToString());
            }
            return commandTexts;
        }
    }
}