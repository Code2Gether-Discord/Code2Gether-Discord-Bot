using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.WebApi.DbContexts;
using Microsoft.AspNetCore.Mvc;

namespace Code2Gether_Discord_Bot.WebApi.Controllers
{
    /// <summary>
    /// A Web API controller that manages the project roles in the Code2Gether Discord Bot's Project Database.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProjectRolesController : Controller
    {
        #region Fields
        private readonly DiscordBotDbContext _dbContext;
        #endregion

        #region Constructor
        public ProjectRolesController(DiscordBotDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region REST API Methods
        /// <summary>
        /// Create a new project role.
        /// </summary>
        /// <param name="projectRoleToAdd">Project role to add to database.</param>
        /// <returns>Action result containing details of added project role.</returns>
        [HttpPost(Name = "PostProjectRole")]
        public async Task<ActionResult<ProjectRole>> AddProjectRoleAsync(ProjectRole projectRoleToAdd)
        {
            if (projectRoleToAdd == null)
                return BadRequest("Project Role is null.");

            // Ensures we don't replace an existing project.
            projectRoleToAdd.ID = 0;

            await _dbContext.ProjectRoles.AddAsync(projectRoleToAdd);
            await _dbContext.SaveChangesAsync();

            var result = CreatedAtAction(actionName: "GetProjectRole",
                routeValues: new { ID = projectRoleToAdd.ID },
                value: projectRoleToAdd);

            return result;
        }

        /// <summary>
        /// Updates the project role with the input ID.
        /// </summary>
        /// <param name="ID">ID of the project role to update.</param>
        /// <param name="projectRoleToUpdate">Project role info to replace the current project role.</param>
        /// <returns>No content.</returns>
        [HttpPut("{ID}", Name = "PutProjectRole")]
        public async Task<ActionResult<ProjectRole>> UpdateProjectRoleAsync(long ID, ProjectRole projectRoleToUpdate)
        {
            var projectRoleToRemove = await _dbContext.ProjectRoles.FindAsync(ID);

            if (projectRoleToRemove == null)
                return NotFound("Unable to find project role.");

            _dbContext.ProjectRoles.Remove(projectRoleToRemove);

            projectRoleToUpdate.ID = ID;
            await _dbContext.ProjectRoles.AddAsync(projectRoleToUpdate);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Get all project roles in the database.
        /// </summary>
        /// <returns>All project roles in the database.</returns>
        [HttpGet(Name = "GetAllProjectRoles")]
        public async Task<ActionResult<IEnumerable<ProjectRole>>> GetAllProjectRolesAsync()
        {
            var projectRole = await _dbContext.ProjectRoles.ToArrayAsync();

            if (projectRole == null)
                return NotFound("Could not find project role.");

            return projectRole;
        }

        /// <summary>
        /// Gets a single project role based on the input ID.
        /// </summary>
        /// <param name="ID">ID of the project role to retrieve.</param>
        /// <returns>The data for the retrieved project role.</returns>
        [HttpGet("{ID}", Name = "GetProjectRole")]
        public async Task<ActionResult<ProjectRole>> GetProjectRoleAsync(long ID)
        {
            return await _dbContext.ProjectRoles.FindAsync(ID);
        }

        /// <summary>
        /// Deletes the project role with the input ID.
        /// </summary>
        /// <param name="ID">The ID of the project role to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{ID}", Name = "DeleteProjectRole")]
        public async Task<ActionResult> DeleteProjectRoleAsync(long ID)
        {
            var projectRoleToDelete = await _dbContext.ProjectRoles.FindAsync(ID);

            if (projectRoleToDelete == null)
                return NotFound("Unable to find project role.");

            _dbContext.ProjectRoles.Remove(projectRoleToDelete);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}
