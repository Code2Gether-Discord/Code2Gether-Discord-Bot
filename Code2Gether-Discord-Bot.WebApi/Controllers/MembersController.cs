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
    public class MembersController : Controller
    {
        #region Fields
        private readonly DiscordBotDbContext _dbContext;
        #endregion

        #region Constructor
        public MembersController(DiscordBotDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region REST API Methods
        /// <summary>
        /// Create a new member.
        /// </summary>
        /// <param name="memberToAdd">Member to add to database.</param>
        /// <returns>Action result containing details of added member.</returns>
        [HttpPost(Name = "PostMember")]
        public async Task<ActionResult<Member>> AddMemberAsync(Member memberToAdd)
        {
            if (memberToAdd == null)
                return BadRequest("User is null.");

            // Ensures we don't replace an existing user.
            memberToAdd.ID = 0;

            await _dbContext.Members.AddAsync(memberToAdd);
            await _dbContext.SaveChangesAsync();

            var result = CreatedAtAction(actionName: "GetMember",
                routeValues: new { ID = memberToAdd.ID },
                value: memberToAdd);

            return result;
        }
        #endregion

        /// <summary>
        /// Updates the user with the input ID.
        /// </summary>
        /// <param name="ID">ID of the member to update.</param>
        /// <param name="memberToUpdate">Member info to replace the current member.</param>
        /// <returns>No content.</returns>
        [HttpPut("{ID}", Name = "PutMember")]
        public async Task<ActionResult<Member>> UpdateMemberAsync(int ID, Member memberToUpdate)
        {
            var memberToRemove = await _dbContext.Members.FindAsync(ID);

            if (memberToRemove == null)
                return NotFound("Unable to find user");

            _dbContext.Members.Remove(memberToRemove);

            memberToUpdate.ID = ID;
            await _dbContext.Members.AddAsync(memberToUpdate);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Get all members in the database.
        /// </summary>
        /// <returns>All members in the database.</returns>
        [HttpGet(Name = "GetAllMembers")]
        public async Task<ActionResult<IEnumerable<Member>>> GetAllMembersAsync()
        {
            return await _dbContext.Members.ToArrayAsync();
        }

        /// <summary>
        /// Gets a single member based on the input ID.
        /// </summary>
        /// <param name="ID">ID of the member to retrieve.</param>
        /// <returns>The data for the retrieved member.</returns>
        [HttpGet("{ID}", Name = "GetMember")]
        public async Task<ActionResult<Member>> GetMemberAsync(long ID)
        {
            var memberToReturn = await _dbContext.Members.FindAsync(ID);

            if (memberToReturn == null)
                return NotFound("Could not find user.");

            return memberToReturn;
        }

        /// <summary>
        /// Deletes the member with the input ID.
        /// </summary>
        /// <param name="ID">The ID of the member to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{ID}", Name = "DeleteUser")]
        public async Task<ActionResult> DeleteMemberASync(long ID)
        {
            var memberToDelete = await _dbContext.Members.FindAsync(ID);

            if (memberToDelete == null)
                return NotFound("Unable to find user.");

            _dbContext.Members.Remove(memberToDelete);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
