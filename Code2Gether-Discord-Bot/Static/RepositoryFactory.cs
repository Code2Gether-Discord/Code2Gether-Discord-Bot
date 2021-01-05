using Code2Gether_Discord_Bot.Library.Models.Repositories;

namespace Code2Gether_Discord_Bot.Static
{
    public static class RepositoryFactory
    {
        public static IProjectRepository GetProjectRepository() =>
            new ProjectDAL(UtilityFactory.GetConfig().ConnectionString);

        public static IMemberRepository GetMemberRepository() =>
            new MemberDAL(UtilityFactory.GetConfig().ConnectionString);
    }
}
