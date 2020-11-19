using System;
using Discord;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public class Logger : ILogger
    {
        private string _typeName { get; set; } = "N/A";

        public Logger() { }

        public Logger(Type classContext)
        {
            _typeName = classContext.Name;
        }

        public void Log(LogSeverity level, string message)
        {
            Console.WriteLine($"[{DateTime.Now}] ({level}) {_typeName} - {message}");   // [10/10/2020 2:20PM] (INFO) - This is a test
        }

        public void Log(LogSeverity level, Exception exception) =>
            Log(level, $"Exception: {exception.Message}{Environment.NewLine}{exception.StackTrace}");

        public void Log(LogSeverity level, string message, Exception exception) =>
            Log(level, $"{message} | Exception: {exception.Message}{Environment.NewLine}{exception.StackTrace}");

        public void Log(ICommandContext context)
        {
            Log(LogSeverity.Info, $"Executed by {context.Message.Author} on {context.Guild} #{context.Channel}");
        }
    }
}
