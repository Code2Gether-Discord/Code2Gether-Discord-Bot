using System;
using Discord;
using Code2Gether_Discord_Bot.Static;
using Code2Gether_Discord_Bot.Models;
using Serilog;

namespace Code2Gether_Discord_Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            var bot = UtilityFactory.GetBot();

            try
            {
                bot.RunAsync().GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                Log.Error(e, "Main process terminated!");
            }
        }
    }
}
