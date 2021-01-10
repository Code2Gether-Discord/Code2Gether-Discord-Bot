﻿using Discord.Commands;
using System;
using Code2Gether_Discord_Bot.Library.BusinessLogic;
using Code2Gether_Discord_Bot.Library.Static;

namespace Code2Gether_Discord_Bot.Static
{
    /// <summary>
    /// Factory in charge of returning specific command logic
    /// This allows the logic for a command to be easily replaced
    /// </summary>
    public static class BusinessLogicFactory
    {
        public static IBusinessLogic GetPingLogic(Type classContext, ICommandContext context, int latency) =>
            new PingLogic(UtilityFactory.GetLogger(classContext), context, latency);

        public static IBusinessLogic GetMakeChannelLogic(Type classContext, ICommandContext context, string newChannelName) =>
            new MakeChannelLogic(UtilityFactory.GetLogger(classContext), context, newChannelName);

        public static IBusinessLogic GetInfoLogic(Type classContext, ICommandContext context) =>
            new InfoLogic(UtilityFactory.GetLogger(classContext), context);

        public static IBusinessLogic GetHelpLogic(Type classContext, ICommandContext context) =>
            new HelpLogic(UtilityFactory.GetLogger(classContext), context, ModuleDetailRepository.Modules, UtilityFactory.GetConfig().Prefix);

        public static IBusinessLogic ExcuseGeneratorLogic(Type classContext, ICommandContext context) =>
            new ExcuseGeneratorLogic(UtilityFactory.GetLogger(classContext), context);
        
        public static IBusinessLogic GetCreateProjectLogic(Type classContext, ICommandContext context, string projectName) =>
            new CreateProjectLogic(UtilityFactory.GetLogger(classContext), context, ManagerFactory.GetProjectManager(), projectName);

        public static IBusinessLogic GetJoinProjectLogic(Type classContext, ICommandContext context, string projectName) =>
            new JoinProjectLogic(UtilityFactory.GetLogger(classContext), context, ManagerFactory.GetProjectManager(), projectName);

        public static IBusinessLogic GetListProjectsLogic(Type classContext, ICommandContext context) =>
            new ListProjectsLogic(UtilityFactory.GetLogger(classContext), context, RepositoryFactory.GetProjectRepository());

        public static IBusinessLogic GetLeetLogic(Type classContext, ICommandContext context, string title) =>
            new LeetLogic(UtilityFactory.GetLogger(classContext), context, title);

        public static IBusinessLogic GetLeetAnsLogic(Type classContext, ICommandContext context, string answer) =>
            new LeetAnsLogic(UtilityFactory.GetLogger(classContext), context, answer);

        public static IBusinessLogic GetGitHubJoinLogic(Type classContext, ICommandContext context, string gitHubEmail) =>
            new GitHubJoinLogic(UtilityFactory.GetLogger(classContext), context, UtilityFactory.GetConfig().GitHubAuthToken, gitHubEmail);
    }
}