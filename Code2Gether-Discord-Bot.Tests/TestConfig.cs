using Code2Gether_Discord_Bot.Library.BusinessLogic;
using Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository;
using Code2Gether_Discord_Bot.Static;
using Code2Gether_Discord_Bot.Tests.Fakes;

namespace Code2Gether_Discord_Bot.Tests
{
    /// <summary>
    /// This class intantiates all fake objects used inside in unit tests.
    /// </summary>
    internal static class TestConfig
    {
        #region BusinessLogicFactory
        /// <summary>
        /// Instantiates a generic <see cref="Library.BusinessLogic.ExcuseGeneratorLogic"/>.
        /// </summary>
        /// <returns>ExcuseGeneratorLogic with irrelevant properties.</returns>
        public static IBusinessLogic ExcuseGeneratorLogic()
        {
            var classContext = typeof(object);
            var context = CommandContext();

            return BusinessLogicFactory.ExcuseGeneratorLogic(classContext, context);
        }
        /// <summary>
        /// Instantiates a generic <see cref="Library.BusinessLogic.InfoLogic"/>.
        /// </summary>
        /// <returns>InfoLogic with irrelevant properties.</returns>
        public static IBusinessLogic InfoLogic()
        {
            var classContext = typeof(object);
            var context = CommandContext();

            return BusinessLogicFactory.GetInfoLogic(classContext, context);
        }
        /// <summary>
        /// Instantiates a generic <see cref="Library.BusinessLogic.JoinProjectLogic"/> with a custom project repository.
        /// </summary>
        /// <param name="projectRepository">This instance's project repository.</param>
        /// <param name="projectName">The name of the project to join.</param>
        /// <returns>JoinProjectLogic with a custom project repository and irrelevant properties.</returns>
        public static IBusinessLogic JoinProjectLogic(IProjectRepository projectRepository, string projectName)
        {
            var context = CommandContext();

            return new JoinProjectLogic(Logger(), context, ProjectManager(projectRepository), projectName);
        }
        /// <summary>
        /// Instantiates a generic <see cref="Library.BusinessLogic.ListProjectsLogic"/> with a custom project repository.
        /// </summary>
        /// <param name="projectRepository">This instance's project repository.</param>
        /// <returns>ListProjectsLogic with a custom project repository and irrelevant properties.</returns>
        public static IBusinessLogic ListProjectsLogic(IProjectRepository projectRepository)
        {
            var context = CommandContext();

            return new ListProjectsLogic(Logger(), context, projectRepository);
        }
        /// <summary>
        /// Instantiates a generic <see cref="Library.BusinessLogic.MakeChannelLogic"/>.
        /// </summary>
        /// <returns>MakeChannelLogic with irrelevant properties.</returns>
        public static IBusinessLogic MakeChannelLogic()
        {
            var classContext = typeof(object);
            var context = CommandContext();
            context.Message = UserMessage(context.Message.Author as FakeUser, "debug!makechannel make-me");
            var channelName = "test-channel";

            return BusinessLogicFactory.GetMakeChannelLogic(classContext, context, channelName);
        }
        /// <summary>
        /// Instantiates a generic <see cref="Library.BusinessLogic.PingLogic"/>.
        /// </summary>
        /// <returns>PingLogic with irrelevant properties.</returns>
        public static IBusinessLogic PingLogic()
        {
            var classContext = typeof(object);
            var context = CommandContext();
            var latency = 1;

            return BusinessLogicFactory.GetPingLogic(classContext, context, latency);
        }
        #endregion

        /// <summary>
        /// Instantiates a FakeProject with generic Name and Author.
        /// </summary>
        /// <param name="id">FakeProject's Id.</param>
        /// <returns>FakeProject with irrelevant Name and Author.</returns>
        public static FakeProject Project(int id) =>
            Project(id, $"proj{id}");
        /// <summary>
        /// Instantiates a FakeProject with custom Id and Author.
        /// </summary>
        /// <param name="id">FakeProject's Id.</param>
        /// <param name="name">FakeProject's Name.</param>
        /// <returns>FakeProject with custom id and author and irrelevant project name.</returns>
        public static FakeProject Project(int id, string name) =>
            Project(id, name, User());
        /// <summary>
        /// Instantiates a FakeProject with custom Id, Name and Author.
        /// </summary>
        /// <param name="id">FakeProject's Id.</param>
        /// <param name="name">FakeProject's Name.</param>
        /// <param name="author">FakeProject's Author.</param>
        /// <returns>FakeProject with all the custom properties.</returns>
        public static FakeProject Project(int id, string name, FakeUser author) =>
            new FakeProject
            (
                id,
                name,
                author
            );
        /// <summary>
        /// Instantiates a FakeProjectManager with empty FakeProjectRepository.
        /// </summary>
        /// <returns>FakeProjectManager with empty project repository.</returns>
        public static FakeProjectManager ProjectManager() =>
            new FakeProjectManager(ProjectRepository());
        /// <summary>
        /// Instantiates a FakeProjectManager with custom IProjectRepository.
        /// </summary>
        /// <param name="projectRepository">FakeProjectManager's project repository.</param>
        /// <returns>FakeProjectManager with custom project repository.</returns>
        public static FakeProjectManager ProjectManager(IProjectRepository projectRepository) =>
            new FakeProjectManager(projectRepository);
        /// <summary>
        /// Instantiates a empty FakeProjectRepository.
        /// </summary>
        /// <returns>Empty FakeProjectRepository.</returns>
        public static FakeProjectRepository ProjectRepository() =>
            new FakeProjectRepository();
        /// <summary>
        /// Intantiates a FakeUser with generic Username, DiscriminatorValue and Id.
        /// </summary>
        /// <returns>FakeUser with irrelevant properties.</returns>
        public static FakeUser User() =>
            User("UnitTest", 1234, 123456789);
        /// <summary>
        /// Intantiates a FakeUser with especific Username, DiscriminatorValue and Id.
        /// </summary>
        /// <param name="username">FakeUser's Username.</param>
        /// <param name="discriminatorValue">FakeUser's DiscriminatorValue.</param>
        /// <param name="id">FakeUser's Id.</param>
        /// <returns>FakeUser with the properties informed in the parameters.</returns>
        public static FakeUser User(string username, ushort discriminatorValue, ulong id) =>
            new FakeUser()
            {
                Username = username,
                DiscriminatorValue = discriminatorValue,
                Id = id
            };
        /// <summary>
        /// Intantiates a generic FakeDiscordClient.
        /// </summary>
        /// <returns>FakeDiscordClient with irrelevant properties.</returns>
        private static FakeDiscordClient Client() =>
            new FakeDiscordClient();
        /// <summary>
        /// Intantiates a generic FakeCommandContext.
        /// </summary>
        /// <returns>FakeCommandContext with irrelevant properties.</returns>
        private static FakeCommandContext CommandContext()
        {
            var commandContext = new FakeCommandContext()
            {
                Channel = MessageChannel(),
                Client = Client(),
                Guild = Guild(),
                User = User()
            };
            commandContext.Message = UserMessage(commandContext.User as FakeUser);

            return commandContext;
        }
        /// <summary>
        /// Intantiates a generic FakeGuild.
        /// </summary>
        /// <returns>FakeGuild with irrelevant properties.</returns>
        private static FakeGuild Guild() =>
            new FakeGuild();
        /// <summary>
        /// Instantiates a FakeLogger that logs nothing.
        /// </summary>
        /// <returns>A non-logger Logger.</returns>
        private static FakeLogger Logger() =>
            new FakeLogger();
        /// <summary>
        /// Instantiates a generic FakeMessageChannel.
        /// </summary>
        /// <returns>FakeMessageChannel with irrelevant properties.</returns>
        private static FakeMessageChannel MessageChannel() =>
            new FakeMessageChannel();
        /// <summary>
        /// Instantiates a generic FakeUserMessage with specific author.
        /// </summary>
        /// <param name="author">Message's author.</param>
        /// <returns>FakeUserMessage with a specific author and irrelevant properties.</returns>
        private static FakeUserMessage UserMessage(FakeUser author) =>
            UserMessage(author, null);
        /// <summary>
        /// Instantiates a generic FakeUserMessage with specific author and content.
        /// </summary>
        /// <param name="author">Message's author.</param>
        /// <param name="content">Message's content.</param>
        /// <returns>FakeUserMessage with a specific author, content and irrelevant properties.</returns>
        private static FakeUserMessage UserMessage(FakeUser author, string content) =>
            new FakeUserMessage()
            {
                Author = author,
                Content = content
            };
    }
}
