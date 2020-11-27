using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.WebApi.DbContexts;
using Microsoft.AspNetCore.Mvc;

namespace Code2Gether_Discord_Bot.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectRoleController : Controller
    {
        #region Fields
        private DiscordBotDbContext _dbContext;
        #endregion

        #region Constructor
        public ProjectRoleController(DiscordBotDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region REST API Methods
        [HttpPost]
        public async Task AddProjectAsync(ProjectRole projectToAdd)
        {
            await _dbContext.ProjectRoles.AddAsync(projectToAdd);
            await _dbContext.SaveChangesAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectRole>> GetAllProjectsAsync()
        {
            return await _dbContext.ProjectRoles.ToArrayAsync();
        }

        [HttpGet("{ID}")]
        public async Task<ProjectRole> GetProjectAsync(string ID)
        {
            long longId;

            if (!long.TryParse(ID, out longId))
            {
                // todo: return error JSON
                return null;
            }

            return await _dbContext.ProjectRoles
                .FirstOrDefaultAsync(x => x.ID == longId);
        }
        #endregion
    }
}
