using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.WebApi.DbContexts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : Controller
    {
        #region Fields
        private DiscordBotDbContext _dbContext;
        #endregion

        #region Constructor
        public ProjectsController(DiscordBotDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        [HttpPost]
        public async Task AddProject(Project modelToAdd)
        {
            await _dbContext.Projects.AddAsync(modelToAdd);
            await _dbContext.SaveChangesAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<Project>> GetAll()
        {
            return await _dbContext.Projects.ToArrayAsync();
        }

        [HttpGet("{ID}")]
        public async Task<Project> Get(string ID)
        {
            long longId;

            if (!long.TryParse(ID, out longId))
            {
                // todo: return error JSON
                return null;
            }
            
            return await _dbContext.Projects
                .FirstOrDefaultAsync(x => x.ID == longId);
        }
    }
}
