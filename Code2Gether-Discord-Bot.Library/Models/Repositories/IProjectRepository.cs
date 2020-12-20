using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Library.Models.Repositories
{
    public interface IProjectRepository : IDataRepository<Project>
    {
        Task<Project> ReadAsync(string projectName);
        Task<bool> AddMemberAsync(Project project, Member member);
        Task<bool> RemoveMemberAsync(Project project, Member member);
    }
}
