﻿using Code2Gether_Discord_Bot.Library.Models;
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

        #region REST API Methods
        [HttpPost]
        public async Task AddProjectAsync(Project projectToAdd)
        {
            await _dbContext.Projects.AddAsync(projectToAdd);
            await _dbContext.SaveChangesAsync();
        }

        [HttpGet]
        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _dbContext.Projects.ToArrayAsync();
        }

        [HttpGet("{ID}")]
        public async Task<Project> GetProjectAsync(string ID)
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
        #endregion
    }
}
