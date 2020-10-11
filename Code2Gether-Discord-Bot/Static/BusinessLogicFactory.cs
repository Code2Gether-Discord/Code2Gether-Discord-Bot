using Code2Gether_Discord_Bot.Library.BusinessLogic;
using Code2Gether_Discord_Bot.Library.Models;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Static
{
    /// <summary>
    /// Factory in charge of returning specific command logic
    /// This allows the logic for a command to be easily replaced
    /// </summary>
    public static class BusinessLogicFactory
    {
        public static IBusinessLogic PingLogic(ICommandContext context, int latency) =>
            new PingLogic(UtilityFactory.GetLogger(), context, latency);

        public static IBusinessLogic InfoLogic(ICommandContext context) =>
            new InfoLogic(UtilityFactory.GetLogger(), context);
    }
}
