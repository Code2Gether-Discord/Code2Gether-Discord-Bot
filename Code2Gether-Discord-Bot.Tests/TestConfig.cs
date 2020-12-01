using Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository;
using Code2Gether_Discord_Bot.Tests.Fakes;

namespace Code2Gether_Discord_Bot.Tests
{
    /// <summary>
    /// This class intantiates all fake objects used inside in unit tests.
    /// </summary>
    internal static class TestConfig
    {
        /// <summary>
        /// Intantiates a FakeProject with generic Name and Author.
        /// </summary>
        /// <param name="id">FakeProject's Id.</param>
        /// <returns>FakeProject with irrelevant Name and Author.</returns>
        public static FakeProject Project(int id) =>
            Project(id, User());
        /// <summary>
        /// Intantiates a FakeProject with custom Id and Author.
        /// </summary>
        /// <param name="id">FakeProject's Id.</param>
        /// <param name="author">FakeProject's Author.</param>
        /// <returns>FakeProject with custom id and author and irrelevant project name.</returns>
        public static FakeProject Project(int id, FakeUser author) =>
            Project(id, $"proj{id}", author);
        /// <summary>
        /// Intantiates a FakeProject with custom Id, Name and Author.
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
        /// Intantiates a FakeProjectManager with empty FakeProjectRepository.
        /// </summary>
        /// <returns>FakeProjectManager with empty project repository.</returns>
        public static FakeProjectManager ProjectManager() =>
            new FakeProjectManager(ProjectRepository());
        /// <summary>
        /// Instantiates a FakeProjectManager with custom IProjectRepository.
        /// </summary>
        /// <param name="projectRepository">FakeProjectManager's project repository.</param>
        /// <returns>FakeProjectManager with custom project repository.</returns>
        public static FakeProjectManager ProjectManager(FakeProjectRepository projectRepository) =>
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

    }
}
