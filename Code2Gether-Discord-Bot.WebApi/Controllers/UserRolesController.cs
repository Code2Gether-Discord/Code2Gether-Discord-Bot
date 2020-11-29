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
    public class UserRolesController : ControllerBase
    {
        #region Fields
        private readonly DiscordBotDbContext _dbContext;
        #endregion

        #region Constructor
        public UserRolesController(DiscordBotDbContext context)
        {
            _dbContext = context;
        }
        #endregion

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        #region REST API Methods
        /// <summary>
        /// Create a new user role..
        /// </summary>
        /// <param name="userToAdd">User role to add to database.</param>
        /// <returns>Action result containing details of added user role.</returns>
        [HttpPost(Name = "PostUserRole")]
        public async Task<ActionResult<UserRole>> AddUserRoleAsync(UserRole userRoleToAdd)
        {
            if (userRoleToAdd == null)
                return BadRequest("User role is null.");

            // Ensures we don't replace an existing user role.
            userRoleToAdd.ID = 0;

            await _dbContext.UserRoles.AddAsync(userRoleToAdd);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetUserRole", new { id = userRoleToAdd.ID }, userRoleToAdd);
        }

        /// <summary>
        /// Updates the user role with the input ID.
        /// </summary>
        /// <param name="ID">ID of the user role to update.</param>
        /// <param name="projectRoleToUpdate">User role info to replace the current user role.</param>
        /// <returns>No content.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRoleAsync(long ID, UserRole userRoleToUpdate)
        {
            var userRoleToRemove = await _dbContext.UserRoles.FindAsync(ID);

            if (userRoleToRemove == null)
                return NotFound("Unable to find user role.");

            _dbContext.UserRoles.Remove(userRoleToRemove);

            userRoleToUpdate.ID = ID;
            await _dbContext.UserRoles.AddAsync(userRoleToUpdate);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Get all user rroles in the database.
        /// </summary>
        /// <returns>All user roles in the database.</returns>
        [HttpGet(Name = "GetAllUserRoles")]
        public async Task<ActionResult<IEnumerable<UserRole>>> GetAllUserRolesAsync()
        {
            return await _dbContext.UserRoles.ToListAsync();
        }

        [HttpGet("Project={ProjectID}", Name = "GetAllUserRolesForProject")]
        public async Task<ActionResult<IEnumerable<UserRole>>> GetAllUserRolesForProjectAsync(string ProjectID)
        {
            return await _dbContext.UserRoles
                .AsAsyncEnumerable()
                .Where(x => x.ProjectID.ToString() == ProjectID)
                .ToArrayAsync();
        }

        /// <summary>
        /// Gets a single user role based on the input ID.
        /// </summary>
        /// <param name="ID">ID of the user role to retrieve.</param>
        /// <returns>The data for the retrieved user role.</returns>
        [HttpGet("{id}", Name = "GetUserRole")]
        public async Task<ActionResult<UserRole>> GetUserRoleAsync(long ID)
        {
            var userRoleToReturn = await _dbContext.UserRoles.FindAsync(ID);

            if(userRoleToReturn == null)
                return NotFound("Could not find user role.");

            return userRoleToReturn;
        }

        /// <summary>
        /// Deletes the user role with the input ID.
        /// </summary>
        /// <param name="ID">The ID of the user role to delete.</param>
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
        #endregion
    }
}
