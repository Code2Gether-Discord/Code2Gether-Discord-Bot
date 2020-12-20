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
            // Hold on to Auhor ID before replacing it in a new Author.
            var tempID = projectToSerialize.Author.ID;

            projectToSerialize.Author = new Member
            {
                ID = tempID
            };

            // Re-serialize members.
            projectToSerialize.Members = projectToSerialize
                .Members
                ?.Select(x => new Member { ID = x.ID })
                .ToList();

            return JsonConvert.SerializeObject(projectToSerialize);
        }

        public async Task<Project> ReadAsync(string projectName)
        {
            var request = new RestRequest($"{_tableRoute}/projectName={projectName}");

            var result = await GetClient().ExecuteGetAsync<Project>(request);

            return result.IsSuccessful ? result.Data : null;
        }
    }
}
