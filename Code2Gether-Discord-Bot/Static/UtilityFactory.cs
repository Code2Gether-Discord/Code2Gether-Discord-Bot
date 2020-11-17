using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Models;
using System;
using System.Runtime.CompilerServices;

namespace Code2Gether_Discord_Bot.Static
{
    /// <summary>
    /// Factory in charge of returning specific utility implementations
    /// This allows the the utility's implementation to be easily replaced
    /// </summary>
    public static class UtilityFactory
    {
        public static ILogger GetLogger(Type classContext) => new Logger(classContext);
        public static IBot GetBot() => new Bot(GetLogger(typeof(Bot)), GetConfig());
        public static IConfig GetConfig() => ConfigProvider.GetConfig();
    }
}
