using System;
using System.Linq;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Library.Models.Repositories
{
    public class ProjectDAL : WebApiDALBase<Project>, IProjectRepository
    {
        protected override string tableRoute => "Projects";

        public ProjectDAL(string connectionString) : base(connectionString) { }

        public async Task<Project> ReadAsync(string projectName)
        {
            var projects = await ReadAllAsync();
            var queriedProject = projects
                .FirstOrDefault(p => p.Name
                    .Equals(projectName));
            return queriedProject;
        }
    }
}
