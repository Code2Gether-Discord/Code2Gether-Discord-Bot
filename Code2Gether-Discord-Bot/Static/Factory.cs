using Code2Gether_Discord_Bot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Code2Gether_Discord_Bot.Static
{
    public static class Factory
    {
        public static ILogger GetLogger() => new Logger();
        public static IBot GetBot() => new Bot(GetLogger());
    }
}
