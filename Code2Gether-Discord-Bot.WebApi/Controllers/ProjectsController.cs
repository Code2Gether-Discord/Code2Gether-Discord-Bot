using System;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.WebApi.DbContexts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.WebApi.Controllers
{
    /// <summary>
    /// A Web API controller that manages the projects in the Code2Gether Discord Bot's Project Database.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : Controller
    {
        #region Fields
        private readonly DiscordBotDbContext _dbContext;
        #endregion

        #region Constructor
        public ProjectsController(DiscordBotDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region REST API Methods
        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="projectToAdd">Project to add to database.</param>
        /// <returns>Action result containing the details of the added project.</returns>
        [HttpPost(Name = "PostProject")]
        public async Task<ActionResult<Project>> AddProjectAsync(Project projectToAdd)
        {
            if (projectToAdd == null)
            {
                return BadRequest("Project is null.");
            }

            // Ensures we don't replace an existing project
            projectToAdd.ID = 0;

            try
            {
                await _dbContext.Projects.AddAsync(projectToAdd);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            var result = CreatedAtAction(actionName: "GetProject",
                routeValues: new { ID = projectToAdd.ID },
                value: projectToAdd);

            return result;
        }

        /// <summary>
        /// Updates the project with the input ID.
        /// </summary>
        /// <param name="ID">ID of the project to update.</param>
        /// <param name="projectToUpdate">Project info to replace the current project.</param>
        /// <returns>No content.</returns>
        [HttpPut("{ID}", Name = "PutProject")]
        public async Task<ActionResult<Project>> GetProjectAsync(string ID, Project projectToUpdate)
        {
            if (!long.TryParse(ID, out var longId))
            {
                return BadRequest("Project ID must be numerical.");
            }

            try
            {
                var projectToRemove = await _dbContext.Projects.FirstOrDefaultAsync(x => x.ID == longId);

                if (projectToRemove == null)
                    return NotFound("Unable to find project.");

                _dbContext.Projects.Remove(projectToRemove);

                projectToUpdate.ID = longId;
                await _dbContext.Projects.AddAsync(projectToUpdate);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }

            return NoContent();
        }
        /// <summary>
        /// Gets all projects in the database.
        /// </summary>
        /// <returns>All projects in the database.</returns>
        [HttpGet(Name = "GetAllProjects")]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjectsAsync()
        {
            return await _dbContext.Projects.ToArrayAsync();
        }

        /// <summary>
        /// Gets a single project based on the input ID.
        /// </summary>
        /// <param name="ID">ID of the project to retrieve.</param>
        /// <returns>The data for the retrieved project.</returns>
        [HttpGet("{ID}", Name = "GetProject")]
        public async Task<ActionResult<Project>> GetProjectAsync(string ID)
        {
            if (!long.TryParse(ID, out var longId))
                return BadRequest("Project ID must be numerical.");

            try
            {
                return await _dbContext.Projects
                    .FirstOrDefaultAsync(x => x.ID == longId);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }

        /// <summary>
        /// Deletes the project with the input ID.
        /// </summary>
        /// <param name="ID">THe ID of the project to delte.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{ID}", Name = "DeleteProject")]
        public async Task<ActionResult> DeleteProjectAsync(string ID)
        {
            if (!long.TryParse(ID, out var longId))
                return BadRequest("Project ID must be numerical.");

            try
            {
                var projectToDelete = await _dbContext.Projects
                    .FirstOrDefaultAsync(x => x.ID == longId);

                if (projectToDelete == null)
                    return NotFound("Unable to find project.");

                _dbContext.Projects.Remove(projectToDelete);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }

            return NoContent();
        }
        #endregion
    }
}
