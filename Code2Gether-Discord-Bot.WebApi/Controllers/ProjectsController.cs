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
        public async Task<ActionResult> AddProjectAsync(Project projectToAdd)
        {
            if (projectToAdd == null)
                return BadRequest("Project is null.");

            // Ensures we don't replace an existing project
            projectToAdd.ID = 0;
            await ProcessAuthorAsync(projectToAdd);

            var query = await _dbContext.Projects.AddAsync(projectToAdd);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Updates the project with the input ID.
        /// </summary>
        /// <param name="ID">ID of the project to update.</param>
        /// <param name="projectToUpdate">Project info to replace the current project.</param>
        /// <returns>No content.</returns>
        [HttpPut("{ID}", Name = "PutProject")]
        public async Task<ActionResult<Project>> UpdateProjectAsync(int ID, Project projectToUpdate)
        {
            var projectToRemove = await _dbContext.Projects.FindAsync(ID);

            if (projectToRemove == null)
                return NotFound("Unable to find project.");

            // Migrate the author and the members to the new project.
            var authorId = projectToRemove.AuthorId;

            var members = await _dbContext.ProjectMembers
                .AsAsyncEnumerable()
                .Where(x => x.ProjectID == projectToUpdate.ID)
                .Select(x => x.MemberID)
                .ToListAsync();

            projectToUpdate.ID = ID;
            await ProcessAuthorAsync(projectToUpdate);

            // Delete the old project.
            _dbContext.Projects.Remove(projectToRemove);
            await _dbContext.SaveChangesAsync();

                        projectToUpdate.AuthorId = authorId;
            await _dbContext.Projects.AddAsync(projectToUpdate);

            foreach(var member in members)
            {
                await _dbContext.ProjectMembers.AddAsync(new ProjectMember
                {
                    ProjectID = projectToUpdate.ID,
                    MemberID = member,
                });
            }

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Gets all projects in the database.
        /// </summary>
        /// <returns>All projects in the database.</returns>
        [HttpGet(Name = "GetAllProjects")]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjectsAsync()
        {
            var projectsToReturn = await _dbContext.Projects.ToArrayAsync();

            foreach (var project in projectsToReturn)
                await JoinMembersAsync(project);

            return projectsToReturn;
        }

        /// <summary>
        /// Gets a single project based on the input ID.
        /// </summary>
        /// <param name="ID">ID of the project to retrieve.</param>
        /// <returns>The data for the retrieved project.</returns>
        [HttpGet("{ID}", Name = "GetProject")]
        public async Task<ActionResult<Project>> GetProjectAsync(int ID)
        {
            var projectToReturn = await _dbContext.Projects.FindAsync(ID);

            if (projectToReturn == null)
                return NotFound($"Could not find project with ID {ID}");

            await JoinMembersAsync(projectToReturn);

            return projectToReturn;
        }

        /// <summary>
        /// Gets a single project based on project name.
        /// </summary>
        /// <param name="projectName">Bame of the project to retrieve.</param>
        /// <returns>The data for the retrieved project.</returns>
        [HttpGet("projectName={projectName}", Name = "GetProjectName")]
        public async Task<ActionResult<Project>> GetProjectAsync(string projectName)
        {
            var projectToReturn = await _dbContext.Projects
                .FirstOrDefaultAsync(x => x.Name == projectName);

            if (projectToReturn == null)
                return NotFound($"Unable to find project with name {projectName}");

            await JoinMembersAsync(projectToReturn);

            return projectToReturn;
        }

        /// <summary>
        /// Add a member to a project.
        /// </summary>
        /// <param name="projectId">ID of project to add a member to.</param>
        /// <param name="memberId">ID of member to add to the project.</param>
        /// <returns></returns>
        [HttpPost("projectId={projectId};memberId={memberId}", Name = "AddMemberToProject")]
        public async Task<ActionResult> AddMemberAsync(int projectId, int memberId)
        {
            var project = _dbContext.ProjectMembers
                .AsAsyncEnumerable()
                .Where(x => x.ProjectID == projectId);

            if (project == null)
                return NotFound($"Could not find project with ID {projectId}");

            var member = await _dbContext.ProjectMembers
                .AsAsyncEnumerable()
                .Where(x => x.ProjectID == projectId)
                .FirstOrDefaultAsync(x => x.MemberID == memberId);

            if (member != null)
                return BadRequest($"Member {memberId} is already in project {projectId}");

            var hasMember = await _dbContext.Members
                .AsAsyncEnumerable()
                .AnyAsync(x => x.ID == memberId);

            if (!hasMember)
                return BadRequest($"Could not find member with ID {memberId}");

            var projectMember = new ProjectMember
            {
                ProjectID = projectId,
                MemberID = memberId,
            };

            await _dbContext.ProjectMembers.AddAsync(projectMember);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("projectId={projectId};memberId={memberId}", Name = "DeleteMemberFromProject")]
        public async Task<ActionResult> RemoveMemberAsync(int projectId, int memberId)
        {
            var projectMemberToDelete = await _dbContext.ProjectMembers
                .AsAsyncEnumerable()
                .Where(x => x.ProjectID == projectId)
                .FirstOrDefaultAsync(x => x.MemberID == memberId);

            if (projectMemberToDelete == null)
                return NotFound($"Could not find project {projectId} with member {memberId}");

            _dbContext.ProjectMembers.Remove(projectMemberToDelete);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes the project with the input ID.
        /// </summary>
        /// <param name="ID">THe ID of the project to delte.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{ID}", Name = "DeleteProject")]
        public async Task<ActionResult> DeleteProjectAsync(int ID)
        {
            var projectToDelete = await _dbContext.Projects.FindAsync(ID);

            if (projectToDelete == null)
                return NotFound("Unable to find project.");

            _dbContext.Projects.Remove(projectToDelete);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region Methods
        private async Task ProcessAuthorAsync(Project projectToAdd)
        {
            var author = await _dbContext.Members.FindAsync(projectToAdd.Author.ID);

            projectToAdd.Author = author;
        }

        private async Task JoinMembersAsync(Project project)
        {
            var author = await _dbContext.Members
                .FindAsync(project.AuthorId);

            project.Author = author;

            var memberIDs = await _dbContext.ProjectMembers
                .AsAsyncEnumerable()
                .Where(x => x.ProjectID == project.ID)
                .Select(x => x.MemberID)
                .ToListAsync();

            var members = await _dbContext.Members
                .AsAsyncEnumerable()
                .Where(x => memberIDs.Contains(x.ID))
                .ToListAsync();

            project.Members = members;
        }
        #endregion
    }
}
