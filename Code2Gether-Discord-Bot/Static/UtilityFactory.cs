using Code2Gether_Discord_Bot.Models;
using Serilog;

namespace Code2Gether_Discord_Bot.Static
{
    /// <summary>
    /// Factory in charge of returning specific utility implementations
    /// This allows the the utility's implementation to be easily replaced
    /// </summary>
    public static class UtilityFactory
    {
        public static IBot GetBot() =>
            new Bot(Log.Logger, GetConfig());

        public static IConfig GetConfig()
            => ConfigProvider.GetConfig();
    }
}
