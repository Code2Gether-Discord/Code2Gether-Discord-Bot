﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models.Repositories;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public class ProjectManager : IProjectManager
    {
        private IProjectRepository _projectRepository;

        public ProjectManager(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        /// <summary>
        /// Checks if a project exists by a given <see cref="projectName"/>.
        /// </summary>
        /// <param name="projectName">Project name to check for</param>
        /// <returns>true if the project exists. false if the project does not exist.</returns>
        public async Task<bool> DoesProjectExistAsync(string projectName)
        {
            return await _projectRepository.ReadAsync(projectName) is not null;
        }

        /// <summary>
        /// Get a project by a given <see cref="projectName"/>
        /// </summary>
        /// <param name="projectName">Project name to search for</param>
        /// <returns>a project if it is found or otherwise returns null</returns>
        public Task<Project> GetProjectAsync(string projectName)
        {
            return _projectRepository.ReadAsync(projectName);
        }

        /// <summary>
        /// Creates a new project with a given name and an given author.
        /// The author is automatically added to the project.
        /// </summary>
        /// <param name="projectName">Name for new project</param>
        /// <param name="author">Member that is requesting project be made</param>
        /// <returns>A new project instance 
        /// or throws an exception if project was not created
        /// or author failed to join the project.</returns>
        public async Task<Project> CreateProjectAsync(string projectName, Member author)
        {
            var newProject = new Project(0, projectName, author);
            if (await _projectRepository.CreateAsync(newProject))
                if (await JoinProjectAsync(projectName, author))    // Author joins new projects by default
                    return newProject;
            throw new Exception($"Failed to create new project: {newProject}!");
        }

        /// <summary>
        /// Attempt to join a project by a given name with a given member.
        /// </summary>
        /// <param name="projectName">Project name to join</param>
        /// <param name="member">Member to join a project</param>
        /// <returns>true if update was successful and the new member is apart of the project
        /// or false if the user is already in the project.</returns>
        public async Task<bool> JoinProjectAsync(string projectName, Member member)
        {
            // Get all projects and store matching one matching projectName
            var project = (await _projectRepository
                .ReadAllAsync())
                .FirstOrDefault(x => x.Name == projectName);

            // If the given member by SnowflakeId does not exist in the project as a member
            if (!project.Members.Any(m => m.SnowflakeId == member.SnowflakeId))
                project.Members.Add(member);
            else
                return false;   // Else they are already in the project

            // Update the project with new member
            var result = await _projectRepository.UpdateAsync(project);

            // Get the updated repository
            project = await _projectRepository.ReadAsync(projectName);

            // Compare if update was successful, and the project now contains the member
            return result && project.Members.Contains(member);
        }
    }
}
