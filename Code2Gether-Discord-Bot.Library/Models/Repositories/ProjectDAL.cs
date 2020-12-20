using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace Code2Gether_Discord_Bot.Library.Models.Repositories
{
    public class ProjectDAL : WebApiDALBase<Project>, IProjectRepository
    {
        protected override string _tableRoute => "Projects";

        public ProjectDAL(string connectionString) : base(connectionString) { }

        protected override string SerializeModel(Project projectToSerialize)
        {
            projectToSerialize.Author = new Member
            {
                ID = projectToSerialize.ID
            };

            projectToSerialize.Members = projectToSerialize
                .Members
                ?.Select(x => new Member { ID = x.ID })
                .ToList();

            var json = JsonConvert.SerializeObject(projectToSerialize);

            return json;
        }

        public async Task<Project> ReadAsync(string projectName)
        {
            /*
            var projects = await ReadAllAsync();

            var queriedProject = projects
                .FirstOrDefault(p => p.Name
                    .Equals(projectName));

            return queriedProject;
             */

            var request = new RestRequest($"{_tableRoute}/projectName={projectName}");

            var result = await GetClient().ExecuteGetAsync<Project>(request);

            return result.IsSuccessful ? result.Data : null;
        }
    }
}
