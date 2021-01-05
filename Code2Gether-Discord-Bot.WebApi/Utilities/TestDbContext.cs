using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.WebApi.DbContexts;
using System.Linq;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.WebApi.Utilities
{
    public static class TestDbContext
    {
        public static async Task TestAsync(this DiscordBotDbContext dbContext)
        {
            /*
            var member = new Member
            {
                SnowflakeId = 12345
            };

            await dbContext.Members.AddAsync(member);
            await dbContext.SaveChangesAsync();
            */

            var retrievedMember = await dbContext.Members.FirstOrDefaultAsync();

            var project = new Project
            {
                Name = "MyProject",
                Author = retrievedMember,
            };

            project.Members.Add(retrievedMember);

            await dbContext.Projects.AddAsync(project);
            await dbContext.SaveChangesAsync();
        }
    }
}
