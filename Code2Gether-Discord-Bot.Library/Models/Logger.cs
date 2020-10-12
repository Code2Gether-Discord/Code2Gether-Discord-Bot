using System;
using Discord;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public interface ILogger
    {
        public void Log(LogSeverity level, string message);
        public void Log(LogSeverity level, string message, Exception exception);
        public void LogCommandUse(ICommandContext context, Type type);
    }

    public class Logger : ILogger
    {
        public void Log(LogSeverity level, string message)
        {
            Console.WriteLine($"[{DateTime.Now}] ({level}) - {message}");   // [10/10/2020 2:20PM] (INFO) - This is a test
        }

        public void Log(LogSeverity level, string message, Exception exception)
        {
            Console.WriteLine($"[{DateTime.Now}] ({level}) - {message}\tException: {exception.Message}");   // [10/10/2020 2:20PM] (INFO) - This is a test Exception: This is an exception
        }

        public void LogCommandUse(ICommandContext context, Type type)
        {
            Log(LogSeverity.Info, $"Logic {type.Name} executed by {context.Message.Author} on {context.Guild} #{context.Channel}");
        }
    }
}
