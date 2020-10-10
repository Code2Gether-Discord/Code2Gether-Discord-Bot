using System;
using Discord;

namespace Code2Gether_Discord_Bot.Models
{
    public interface ILogger
    {
        public void Log(LogSeverity level, string message);
        public void Log(LogSeverity level, string message, Exception exception);
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
    }
}
