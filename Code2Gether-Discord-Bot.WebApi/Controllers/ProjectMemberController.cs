using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.WebApi.DbContexts;

namespace Code2Gether_Discord_Bot.WebApi.Controllers
{
    /// <summary>
    /// A Web API controller that manages the user roles in the Code2Gether Discord Bot's Project Database.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProjectMemberController : ControllerBase
    {
        #region Fields
        private readonly DiscordBotDbContext _dbContext;
        #endregion

        #region Constructor
        public ProjectMemberController(DiscordBotDbContext context)
        {
            _dbContext = context;
        }
        #endregion

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        #region REST API Methods
        /// <summary>
        /// Create a new project member..
        /// </summary>
        /// <param name="userToAdd">Project member to add to database.</param>
        /// <returns>Action result containing details of added project member.</returns>
        [HttpPost(Name = "PostProjectMember")]
        public async Task<ActionResult<ProjectMember>> AddUserRoleAsync(ProjectMember userRoleToAdd)
        {
            if (userRoleToAdd == null)
                return BadRequest("User role is null.");

            await _dbContext.UserRoles.AddAsync(userRoleToAdd);
            await _dbContext.SaveChangesAsync();

            return NoContent();
            // return CreatedAtAction("GetUserRole", new { id = userRoleToAdd.ID }, userRoleToAdd);
        }

        /// <summary>
        /// Get all project members in the database.
        /// </summary>
        /// <returns>All project members in the database.</returns>
        [HttpGet(Name = "GetAllProjectMembers")]
        public async Task<ActionResult<IEnumerable<ProjectMember>>> GetAllProjectMembersAsync()
        {
            return await _dbContext.UserRoles.ToListAsync();
        }

        /*

        /// <summary>
        /// Gets a single project member based on the input ID.
        /// </summary>
        /// <param name="ID">ID of the project member to retrieve.</param>
        /// <returns>The data for the retrieved project member.</returns>
        [HttpGet("{id}", Name = "GetProjectMember")]
        public async Task<ActionResult<ProjectMember>> GetProjectMemberAsync(long ID)
        {
            var userRoleToReturn = await _dbContext.UserRoles.FindAsync(ID);

            if(userRoleToReturn == null)
                return NotFound("Could not find user role.");

            return userRoleToReturn;
        }

        /// <summary>
        /// Deletes the user role with the input ID.
        /// </summary>
        /// <param name="ID">The ID of the project member to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRoleAsync(long ID)
        {
            var projectUserProjectRole = await _dbContext.UserRoles.FindAsync(ID);

            if (projectUserProjectRole == null)
                return NotFound("Unable to find user role.");

            _dbContext.UserRoles.Remove(projectUserProjectRole);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        */

        #endregion
    }
}
