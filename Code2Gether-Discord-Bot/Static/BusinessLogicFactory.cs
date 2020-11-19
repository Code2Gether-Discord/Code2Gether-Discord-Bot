using Code2Gether_Discord_Bot.Library.BusinessLogic;
using Code2Gether_Discord_Bot.Library.Models;
using Discord.Commands;
using System;

namespace Code2Gether_Discord_Bot.Static
{
    /// <summary>
    /// Factory in charge of returning specific command logic
    /// This allows the logic for a command to be easily replaced
    /// </summary>
    public static class BusinessLogicFactory
    {
        public static IBusinessLogic PingLogic(Type classContext, ICommandContext context, int latency) =>
            new PingLogic(UtilityFactory.GetLogger(classContext), context, latency);

        public static IBusinessLogic MakeChannelLogic(Type classContext, ICommandContext context, string newChannelName) =>
            new MakeChannelLogic(UtilityFactory.GetLogger(classContext), context, newChannelName);

        public static IBusinessLogic InfoLogic(Type classContext, ICommandContext context) =>
            new InfoLogic(UtilityFactory.GetLogger(classContext), context);

        public static IBusinessLogic HelpLogic(Type classContext, ICommandContext context) =>
            new HelpLogic(UtilityFactory.GetLogger(classContext), context, ModuleDetailRepository.Modules, UtilityFactory.GetConfig().Prefix);
    }
}
