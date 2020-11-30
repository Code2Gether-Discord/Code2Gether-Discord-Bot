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

            await _dbContext.ProjectMembers.AddAsync(userRoleToAdd);
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
            return await _dbContext.ProjectMembers.ToListAsync();
        }

        /// <summary>
        /// Gets project members belonging to input Project ID.
        /// </summary>
        /// <param name="ProjectID">Project ID to filter the project members by.</param>
        /// <returns>The project members belonging to the input project.</returns>
        [HttpGet("project={ProjectID}", Name = "GetMembersForProject")]
        public async Task<ActionResult<IEnumerable<ProjectMember>>> GetMembersForProjectAsync(int ProjectID)
        {
            var projectMembersToReturn = await _dbContext
                .ProjectMembers
                .AsAsyncEnumerable()
                .Where(x => x.ProjectID == ProjectID)
                .ToListAsync();

            return projectMembersToReturn;
        }

        /// <summary>
        /// Gets project members that have the input Member ID.
        /// </summary>
        /// <param name="MemberID">Member ID to filter the project members by.</param>
        /// <returns>The project members with the input member.</returns>
        [HttpGet("member={MemberID}", Name = "GetProjectsForMember")]
        public async Task<ActionResult<IEnumerable<ProjectMember>>> GetProjectsForMemberAsync(int MemberID)
        {
            var projectMembersToReturn = await _dbContext
                .ProjectMembers
                .AsAsyncEnumerable()
                .Where(x => x.MemberID == MemberID)
                .ToListAsync();

            return projectMembersToReturn;
        }

        [HttpGet("project={ProjectID}/member={MemberID}", Name = "GetProjectMember")]
        public async Task<ActionResult<ProjectMember>> GetProjectMemberAsync(int ProjectID, int MemberID)
        {
            var projectMemberToReturn = await _dbContext
                .ProjectMembers
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.MemberID == MemberID && x.ProjectID == ProjectID);

            if (projectMemberToReturn == null)
                return NotFound("Umable to find project member.");

            return projectMemberToReturn;
        }

        /// <summary>
        /// Deletes the user role with the input ID.
        /// </summary>
        /// <param name="ID">The ID of the project member to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("project={ProjectID}/member={MemberID}", Name = "GetProjectMember")]
        public async Task<IActionResult> DeleteProjectMemberAsync(int ProjectID, int MemberID)
        {
            var projectMemberToDelete = await _dbContext
                .ProjectMembers
                .AsAsyncEnumerable()
                .FirstOrDefaultAsync(x => x.MemberID == MemberID && x.ProjectID == ProjectID);

            if (projectMemberToDelete == null)
                return NotFound("Unable to find project member.");

            _dbContext.ProjectMembers.Remove(projectMemberToDelete);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        #endregion
    }
}
