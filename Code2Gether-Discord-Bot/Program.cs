using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Code2Gether_Discord_Bot.Static;
using Code2Gether_Discord_Bot.Models;

namespace Code2Gether_Discord_Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = Factory.GetLogger();
            IBot bot = Factory.GetBot();

            while (true)
            {
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
}
