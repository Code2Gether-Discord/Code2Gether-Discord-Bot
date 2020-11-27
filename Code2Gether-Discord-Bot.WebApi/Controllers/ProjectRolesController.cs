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
    public class ProjectRolesController : Controller
    {
        /// <summary>
        /// A Web API controller that manages the Roles in the Code2Gether Discord Bot's Project Database.
        /// </summary>
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
        public async Task<ActionResult<ProjectRole>> AddProjectAsync(ProjectRole projectRoleToAdd)
        {
            if (projectRoleToAdd == null)
                return BadRequest("Project Role is null.");

            // Ensures we don't replace an existing project.
            projectRoleToAdd.ID = 0;

            try
            {
                await _dbContext.ProjectRoles.AddAsync(projectRoleToAdd);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Occurs if issue with database.
                return Problem(ex.Message, statusCode: 500);
            }

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
        public async Task<ActionResult<ProjectRole>> UpdateProjectRoleAsync(string ID, ProjectRole projectRoleToUpdate)
        {
            if (!long.TryParse(ID, out var longId))
                return BadRequest("Project Role ID must be numerical.");

            try
            {
                var projectRoleToRemove = await _dbContext.ProjectRoles.FirstOrDefaultAsync(x => x.ID == longId);

                if (projectRoleToRemove == null)
                    return NotFound("Unable to find project role.");

                _dbContext.ProjectRoles.Remove(projectRoleToRemove);

                projectRoleToUpdate.ID = longId;
                await _dbContext.ProjectRoles.AddAsync(projectRoleToUpdate);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }

            return NoContent();
        }

        /// <summary>
        /// Get all project roles in the database.
        /// </summary>
        /// <returns>All project roles in the database.</returns>
        [HttpGet(Name = "GetAllProjectRoles")]
        public async Task<ActionResult<IEnumerable<ProjectRole>>> GetAllProjectsAsync()
        {
            try
            {
                return await _dbContext.ProjectRoles.ToArrayAsync();
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }

        
        /// <summary>
        /// Gets a single project role based on the input ID.
        /// </summary>
        /// <param name="ID">ID of the project role to retrieve.</param>
        /// <returns>The data for the retrieved project role.</returns>
        [HttpGet("{ID}", Name = "GetProjectRole")]
        public async Task<ActionResult<ProjectRole>> GetProjectAsync(string ID)
        {
            if (!long.TryParse(ID, out var longId))
                return BadRequest("Project Role ID must be numerical.");

            try
            {
                return await _dbContext.ProjectRoles
                .FirstOrDefaultAsync(x => x.ID == longId);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }

        /// <summary>
        /// Deletes the project role with the input ID.
        /// </summary>
        /// <param name="ID">The ID of the project role to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{ID}", Name = "DeleteProjectRole")]
        public async Task<ActionResult> DeleteProjectRoleAsync(string ID)
        {
            if (!long.TryParse(ID, out var longId))
                return BadRequest("Project Role ID must be numerical.");

            try
            {
                var projectRoleToDelete = await _dbContext.ProjectRoles
                    .FirstOrDefaultAsync(x => x.ID == longId);

                if (projectRoleToDelete == null)
                    return NotFound("Unable to find project role.");

                _dbContext.ProjectRoles.Remove(projectRoleToDelete);
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }

            return NoContent();
        }
        #endregion
    }
}
