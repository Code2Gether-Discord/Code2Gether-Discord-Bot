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
    /// A Web API controller that manages the users in the Code2Gether Discord Bot's Project Database.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        #region Fields
        private readonly DiscordBotDbContext _dbContext;
        #endregion

        #region Constructor
        public UsersController(DiscordBotDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region REST API Methods
        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="userToAdd">User to add to database.</param>
        /// <returns>Action result containing details of added user.</returns>
        [HttpPost(Name = "PostUser")]
        public async Task<ActionResult<User>> AddUserAsync(User userToAdd)
        {
            if (userToAdd == null)
                return BadRequest("User is null.");

            // Ensures we don't replace an existing user.
            userToAdd.ID = 0;

            await _dbContext.Users.AddAsync(userToAdd);
            await _dbContext.SaveChangesAsync();

            var result = CreatedAtAction(actionName: "GetUser",
                routeValues: new { ID = userToAdd.ID },
                value: userToAdd);

            return result;
        }
        #endregion

        /// <summary>
        /// Updates the user with the input ID.
        /// </summary>
        /// <param name="ID">ID of the user to update.</param>
        /// <param name="userToUpdate">User info to replace the current user.</param>
        /// <returns>No content.</returns>
        [HttpPut("{ID}", Name = "PutUser")]
        public async Task<ActionResult<User>> UpdateUserAsync(long ID, User userToUpdate)
        {
            var userToRemove = await _dbContext.Users.FindAsync(ID);

            if (userToRemove == null)
                return NotFound("Unable to find user");

            _dbContext.Users.Remove(userToRemove);

            userToUpdate.ID = ID;
            await _dbContext.Users.AddAsync(userToUpdate);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Get all users in the database.
        /// </summary>
        /// <returns>All users in the database.</returns>
        [HttpGet(Name = "GetAllUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToArrayAsync();
        }

        /// <summary>
        /// Gets a single user based on the input ID.
        /// </summary>
        /// <param name="ID">ID of the user to retrieve.</param>
        /// <returns>The data for the retrieved user.</returns>
        [HttpGet("{ID}", Name = "GetUser")]
        public async Task<ActionResult<User>> GetUserAsync(long ID)
        {
            var userToReturn = await _dbContext.Users.FindAsync(ID);

            if (userToReturn == null)
                return NotFound("Could not find user.");

            return userToReturn;
        }

        /// <summary>
        /// Deletes the user with the input ID.
        /// </summary>
        /// <param name="ID">The ID of the user to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{ID}", Name = "DeleteUser")]
        public async Task<ActionResult> DeleteUserAsync(long ID)
        {
            var userToDelete = await _dbContext.Users.FindAsync(ID);

            if (userToDelete == null)
                return NotFound("Unable to find user.");

            _dbContext.Users.Remove(userToDelete);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
