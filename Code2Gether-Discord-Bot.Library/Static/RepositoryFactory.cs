using System.Reflection.Metadata.Ecma335;
using Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository;

namespace Code2Gether_Discord_Bot.Library.Static
{
    public static class RepositoryFactory
    {
        // TODO: replace with actual database, and remove this
        private static IProjectRepository instance = GetProjectRepository();

        public static IProjectRepository GetProjectRepository() =>
            instance != null ? instance : new FakeProjectDAL();

        //public static IProjectRepository GetProjectRepository() =>
        //    new ProjectDAL();
    }
}
