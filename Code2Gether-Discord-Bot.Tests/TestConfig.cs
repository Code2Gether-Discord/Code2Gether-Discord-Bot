using Code2Gether_Discord_Bot.Library.BusinessLogic;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository;
using Code2Gether_Discord_Bot.Tests.Fakes;

namespace Code2Gether_Discord_Bot.Tests
{
    /// <summary>
    /// This class intantiates all fake objects used inside in unit tests.
    /// </summary>
    internal static class TestConfig
    {
        #region BusinessLogic
        /// <summary>
        /// Instantiates a generic <see cref="Library.BusinessLogic.ExcuseGeneratorLogic"/>.
        /// </summary>
        /// <returns>ExcuseGeneratorLogic with irrelevant properties.</returns>
        public static IBusinessLogic ExcuseGeneratorLogic()
        {
            var logger = Logger();
            var context = CommandContext();

            return new ExcuseGeneratorLogic(logger, context);
        }
        /// <summary>
        /// Instantiates a generic <see cref="Library.BusinessLogic.InfoLogic"/>.
        /// </summary>
        /// <returns>InfoLogic with irrelevant properties.</returns>
        public static IBusinessLogic InfoLogic()
        {
            var logger = Logger();
            var context = CommandContext();

            return new InfoLogic(logger, context);
        }
        /// <summary>
        /// Instantiates a generic <see cref="Library.BusinessLogic.JoinProjectLogic"/> with a custom project repository.
        /// </summary>
        /// <param name="projectRepository">This instance's project repository.</param>
        /// <param name="projectName">The name of the project to join.</param>
        /// <returns>JoinProjectLogic with a custom project repository and irrelevant properties.</returns>
        public static IBusinessLogic JoinProjectLogic(IProjectRepository projectRepository, string projectName)
        {
            var logger = Logger();
            var context = CommandContext();
            var projectManager = ProjectManager(projectRepository);

            return new JoinProjectLogic(logger, context, projectManager, projectName);
        }
        /// <summary>
        /// Instantiates a generic <see cref="Library.BusinessLogic.ListProjectsLogic"/> with a custom project repository.
        /// </summary>
        /// <param name="projectRepository">This instance's project repository.</param>
        /// <returns>ListProjectsLogic with a custom project repository and irrelevant properties.</returns>
        public static IBusinessLogic ListProjectsLogic(IProjectRepository projectRepository)
        {
            var logger = Logger();
            var context = CommandContext();

            return new ListProjectsLogic(logger, context, projectRepository);
        }
        /// <summary>
        /// Instantiates a generic <see cref="Library.BusinessLogic.MakeChannelLogic"/>.
        /// </summary>
        /// <returns>MakeChannelLogic with irrelevant properties.</returns>
        public static IBusinessLogic MakeChannelLogic()
        {
            var logger = Logger();
            var context = CommandContext();
            context.Message = UserMessage(context.Message.Author as FakeUser, "debug!makechannel make-me");
            var channelName = "test-channel";

            return new MakeChannelLogic(logger, context, channelName);
        }
        /// <summary>
        /// Instantiates a generic <see cref="Library.BusinessLogic.PingLogic"/>.
        /// </summary>
        /// <returns>PingLogic with irrelevant properties.</returns>
        public static IBusinessLogic PingLogic()
        {
            var logger = Logger();
            var context = CommandContext();
            var latency = 1;

            return new PingLogic(logger, context, latency);
        }
        #endregion

        /// <summary>
        /// Instantiates a Project with generic Name and Author.
        /// </summary>
        /// <param name="id">Project's Id.</param>
        /// <returns>Project with irrelevant Name and Author.</returns>
        public static Project Project(int id) =>
            Project(id, $"proj{id}");
        /// <summary>
        /// Instantiates a Project with custom Id and Author.
        /// </summary>
        /// <param name="id">Project's Id.</param>
        /// <param name="name">Project's Name.</param>
        /// <returns>Project with custom id and author and irrelevant project name.</returns>
        public static Project Project(int id, string name) =>
            Project(id, name, User());
        /// <summary>
        /// Instantiates a Project with custom Id, Name and Author.
        /// </summary>
        /// <param name="id">Project's Id.</param>
        /// <param name="name">Project's Name.</param>
        /// <param name="author">Project's Author.</param>
        /// <returns>Project with all the custom properties.</returns>
        public static Project Project(int id, string name, FakeUser author) =>
            new Project
            (
                id,
                name,
                author
            );
        /// <summary>
        /// Instantiates a ProjectManager with empty FakeProjectRepository.
        /// </summary>
        /// <returns>ProjectManager with empty project repository.</returns>
        public static ProjectManager ProjectManager() =>
            new ProjectManager(ProjectRepository());
        /// <summary>
        /// Instantiates a ProjectManager with custom IProjectRepository.
        /// </summary>
        /// <param name="projectRepository">ProjectManager's project repository.</param>
        /// <returns>ProjectManager with custom project repository.</returns>
        public static ProjectManager ProjectManager(IProjectRepository projectRepository) =>
            new ProjectManager(projectRepository);
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
