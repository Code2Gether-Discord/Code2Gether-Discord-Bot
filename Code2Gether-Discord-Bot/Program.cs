using System;
using Discord;
using Code2Gether_Discord_Bot.Static;
using Code2Gether_Discord_Bot.Models;
using Code2Gether_Discord_Bot.Library.Models;

namespace Code2Gether_Discord_Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = UtilityFactory.GetLogger();
            IBot bot = UtilityFactory.GetBot();

            try
            {
                bot.RunAsync().GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                logger.Log(LogSeverity.Critical, $"Main process terminated! Exception: {e.Message}");
            }
        }
    }
}
