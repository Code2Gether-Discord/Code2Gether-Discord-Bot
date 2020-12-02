using Code2Gether_Discord_Bot.Library.Models;
using Discord;
using Discord.Commands;
using System;

namespace Code2Gether_Discord_Bot.Tests.Fakes
{
    /// <summary>
    /// Just a logger that logs nothing ¯\_(ツ)_/¯
    /// </summary>
    class FakeLogger : ILogger
    {
        public void Log(LogSeverity level, string message) { }

        public void Log(LogSeverity level, Exception exception) { }

        public void Log(LogSeverity level, string message, Exception exception) { }

        public void Log(ICommandContext context) { }
    }
}
