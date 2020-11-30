﻿using System;
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
                return BadRequest("Project is null.");

            // Ensures we don't replace an existing project
            projectToAdd.ID = 0;

            await _dbContext.Projects.AddAsync(projectToAdd);
            await _dbContext.SaveChangesAsync();

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
        public async Task<ActionResult<Project>> GetProjectAsync(int ID, Project projectToUpdate)
        {
            var projectToRemove = await _dbContext.Projects.FindAsync(ID);

            if (projectToRemove == null)
                return NotFound("Unable to find project.");

            _dbContext.Projects.Remove(projectToRemove);

            projectToUpdate.ID = ID;
            await _dbContext.Projects.AddAsync(projectToUpdate);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Gets all projects in the database.
        /// </summary>
        /// <returns>All projects in the database.</returns>
        [HttpGet(Name = "GetAllProjects")]
        public async Task<ActionResult<IEnumerable<ProjectOutput>>> GetAllProjectsAsync()
        {
            var projects = await _dbContext.Projects.ToArrayAsync();

            var outputTasks = projects.Select(x => getProjectOutputAsync(x));

            return await Task.WhenAll(outputTasks);
        }

        /// <summary>
        /// Gets a single project based on the input ID.
        /// </summary>
        /// <param name="ID">ID of the project to retrieve.</param>
        /// <returns>The data for the retrieved project.</returns>
        [HttpGet("{ID}", Name = "GetProject")]
        public async Task<ActionResult<ProjectOutput>> GetProjectAsync(int ID)
        {
            var projectToReturn = await _dbContext.Projects.FindAsync(ID);

            if (projectToReturn == null)
                return NotFound("Could not find project.");

            return await getProjectOutputAsync(projectToReturn);
        }

        /// <summary>
        /// Deletes the project with the input ID.
        /// </summary>
        /// <param name="ID">THe ID of the project to delte.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{ID}", Name = "DeleteProject")]
        public async Task<ActionResult> DeleteProjectAsync(long ID)
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
        private async Task<ProjectOutput> getProjectOutputAsync(Project project)
        {
            var userSnowflakes = await _dbContext
                .ProjectMembers
                .AsAsyncEnumerable()
                .Where(x => x.ProjectID == project.ID)
                .Select(x => x.Member.SnowflakeId)
                .ToListAsync();

            return new ProjectOutput(project, userSnowflakes);
        }
        #endregion
    }

    public record ProjectOutput(Project Project, IList<ulong> MemberSnowflakeIds);
}
