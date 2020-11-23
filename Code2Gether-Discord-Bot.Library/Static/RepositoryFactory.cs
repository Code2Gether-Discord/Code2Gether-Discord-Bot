using Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository;

namespace Code2Gether_Discord_Bot.Library.Static
{
    public static class RepositoryFactory
    {
        public static IProjectRepository GetProjectRepository() =>
            new ProjectDAL();
    }
}
