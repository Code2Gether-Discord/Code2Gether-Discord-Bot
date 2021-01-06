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

            var memberDal = new MemberDataAccessLayer(connectionString);
            var projectDal = new ProjectDataAccessLayer(connectionString);

            // Create a new member.
            var member = new Member
            {
                SnowflakeId = 12345
            };

            await memberDal.CreateAsync(member);

            var memberRetrieved = (await memberDal.ReadAllAsync()).FirstOrDefault();

            // Create a new project with the member as an author.
            var project = new Project
            {
                Name = "MyProject",
                Author = memberRetrieved,
            };

            project.Members.Add(memberRetrieved);

            await projectDal.CreateAsync(project);

            // Create another new member.
            var member2 = new Member
            {
                SnowflakeId = 23456
            };

            await memberDal.CreateAsync(member2);

            var member2Retrieved = await memberDal.ReadFromSnowflakeAsync(23456);

            // Add new member to project, and update.
            var project2 = await projectDal.ReadAsync("MyProject");

            project2.Members.Add(member2Retrieved);

            await projectDal.UpdateAsync(project2);
        }
    }
}
