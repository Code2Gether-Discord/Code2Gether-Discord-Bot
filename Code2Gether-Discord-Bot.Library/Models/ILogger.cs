using System;
using Discord;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public interface ILogger
    {
        public void Log(LogSeverity level, string message);
        public void Log(LogSeverity level, string message, Exception exception);
        public void LogCommandUse(ICommandContext context);
    }
}
