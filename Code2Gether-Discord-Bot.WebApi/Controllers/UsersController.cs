using System;
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
    public class UsersController : Controller
    {
        #region Fields
        private DiscordBotDbContext _dbContext;
        #endregion

        #region Constructor
        public UsersController(DiscordBotDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region REST API Methods
        [HttpPost]
        public async Task<ActionResult<User>> AddUserAsync(User userToAdd)
        {
            if(userToAdd == null)
            {
                return BadRequest("User is null.");
            }

            // Ensures we don't replace an existing user.
            userToAdd.ID = 0;

            try
            {
                await _dbContext.Users.AddAsync(userToAdd);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            return CreatedAtAction(actionName: "GetUser", routeValues: new { id = userToAdd.ID }, value: userToAdd);
        }
        #endregion

        [HttpPut("{ID}")]
        public async Task<ActionResult<User>> UpdateUserAsync(string ID, User userToUpdate)
        {
            long longId;

            if(!long.TryParse(ID, out longId))
            {
                return BadRequest("User ID must be numerical.");
            }

            var userToRemove = await _dbContext.Users.FirstOrDefaultAsync(x => x.ID == longId);

            if(userToRemove == null)
            {
                return NotFound("Unable to find user");
            }

            _dbContext.Users.Remove(userToRemove);
            
            userToUpdate.ID = longId;
            await _dbContext.Users.AddAsync(userToUpdate);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToArrayAsync();
        }

        [HttpGet("{ID}", Name = "GetUser")]
        public async Task<ActionResult<User>> GetUserAsync(string ID)
        {
            long longId;

            if (!long.TryParse(ID, out longId))
            {
                return BadRequest("User ID must be numerical.");
            }

            return await _dbContext.Users
                .FirstOrDefaultAsync(x => x.ID == longId);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> DeleteUserAsync(string ID)
        {
            long longId;

            if (!long.TryParse(ID, out longId))
            {
                return BadRequest("User ID must be numerical.");
            }

            var userToDelete = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.ID == longId);

            if (userToDelete == null)
            {
                return NotFound("Unable to find user.");
            }

            _dbContext.Users.Remove(userToDelete);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
