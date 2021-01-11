using Discord.Commands;
using System;
using Code2Gether_Discord_Bot.Library.BusinessLogic;
using Code2Gether_Discord_Bot.Library.Static;
using Serilog;

namespace Code2Gether_Discord_Bot.Static
{
    /// <summary>
    /// Factory in charge of returning specific command logic
    /// This allows the logic for a command to be easily replaced
    /// </summary>
    public static class BusinessLogicFactory
    {
        public static IBusinessLogic GetPingLogic(Type classContext, ICommandContext context, int latency) =>
            new PingLogic(Log.Logger.ForContext(classContext), context, latency);

        public static IBusinessLogic GetMakeChannelLogic(Type classContext, ICommandContext context, string newChannelName) =>
            new MakeChannelLogic(Log.Logger.ForContext(classContext), context, newChannelName);

        public static IBusinessLogic GetInfoLogic(Type classContext, ICommandContext context) =>
            new InfoLogic(Log.Logger.ForContext(classContext), context);

        public static IBusinessLogic GetHelpLogic(Type classContext, ICommandContext context) =>
            new HelpLogic(Log.Logger.ForContext(classContext), context, ModuleDetailRepository.Modules, UtilityFactory.GetConfig().Prefix);

        public static IBusinessLogic ExcuseGeneratorLogic(Type classContext, ICommandContext context) =>
            new ExcuseGeneratorLogic(Log.Logger.ForContext(classContext), context);
        
        public static IBusinessLogic GetCreateProjectLogic(Type classContext, ICommandContext context, string arguments) =>
            new CreateProjectLogic(Log.Logger.ForContext(classContext), context, ManagerFactory.GetProjectManager(), arguments);

        public static IBusinessLogic GetJoinProjectLogic(Type classContext, ICommandContext context, string arguments) =>
            new JoinProjectLogic(Log.Logger.ForContext(classContext), context, ManagerFactory.GetProjectManager(), arguments);

        public static IBusinessLogic GetListProjectsLogic(Type classContext, ICommandContext context) =>
            new ListProjectsLogic(Log.Logger.ForContext(classContext), context, RepositoryFactory.GetProjectRepository());

        public static IBusinessLogic GetLeetLogic(Type classContext, ICommandContext context, string title) =>
            new LeetLogic(Log.Logger.ForContext(classContext), context, title);

        public static IBusinessLogic GetLeetAnsLogic(Type classContext, ICommandContext context, string answer) =>
            new LeetAnsLogic(Log.Logger.ForContext(classContext), context, answer);
    }
}
