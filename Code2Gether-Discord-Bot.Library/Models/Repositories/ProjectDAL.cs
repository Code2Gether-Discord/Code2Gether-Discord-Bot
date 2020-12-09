using System;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Library.Models.Repositories
{
    public class ProjectDAL : WebApiDALBase<Project>, IProjectRepository
    {
        protected override string tableRoute => "Projects";

        public ProjectDAL(string connectionString) : base(connectionString) { }

        public Task<Project> ReadAsync(string projectName)
        {
            throw new NotImplementedException();
        }
    }
}
