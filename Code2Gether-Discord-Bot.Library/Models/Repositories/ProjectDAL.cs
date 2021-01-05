using System.Threading.Tasks;
using RestSharp;

namespace Code2Gether_Discord_Bot.Library.Models.Repositories
{
    public class ProjectDAL : WebApiDALBase<Project>, IProjectRepository
    {
        protected override string _tableRoute => "Projects";

        public ProjectDAL(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Retrieves project based on project name.
        /// </summary>
        /// <param name="projectName">Name of project to retrieve.</param>
        /// <returns>Data for project to retrieve. Null if not found.</returns>
        public async Task<Project> ReadAsync(string projectName)
        {
            var request = new RestRequest($"{_tableRoute}/projectName={projectName}");

            var result = await GetClient().ExecuteGetAsync<Project>(request);

            return result.IsSuccessful ? result.Data : null;
        }

        /// <summary>
        /// Adds a member to a project.
        /// </summary>
        /// <param name="project">Project of member to add.</param>
        /// <param name="member">Member to add to project.</param>
        /// <returns>True if add is successful.</returns>
        public async Task<bool> AddMemberAsync(Project project, Member member)
        {
            var request = new RestRequest($"{_tableRoute}/projectId={project.ID};memberId={member.ID}");

            var result = await GetClient().ExecutePostAsync<Project>(request);

            return result.IsSuccessful;
        }

        /// <summary>
        /// Adds a member to a project.
        /// </summary>
        /// <param name="project">Project of member to delete.</param>
        /// <param name="member">Member to delete from project.</param>
        /// <returns>True if delete is successful.</returns>
        public async Task<bool> RemoveMemberAsync(Project project, Member member)
        {
            var request = new RestRequest($"{_tableRoute}/projectId={project.ID};memberId={member.ID}", Method.DELETE);

            var result = await GetClient().ExecuteAsync<Project>(request);

            return result.IsSuccessful;
        }
    }
}
