using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.Repositories; 
using System.Linq;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.ConsoleDiagnostics
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = "https://localhost:5001/";

            var memberDal = new MemberDAL(connectionString);
            var projectDal = new ProjectDAL(connectionString);

            var member = new Member
            {
                SnowflakeId = 12345
            };

            await memberDal.CreateAsync(member);

            var memberRetrieved = (await memberDal.ReadAllAsync()).FirstOrDefault();

            var project = new Project
            {
                Name = "MyProject",
                Author = memberRetrieved,
            };

            project.Members.Add(memberRetrieved);

            await projectDal.CreateAsync(project);
        }
    }
}
