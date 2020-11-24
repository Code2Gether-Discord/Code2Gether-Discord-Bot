using System;
using System.Linq;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository;
using Discord;
using SQLitePCL;

namespace Code2Gether_Discord_Bot.Library.Static
{
    public static class ProjectManager
    {
        public static bool DoesProjectExist(IProjectRepository projectRepository, string projectName) =>
            projectRepository.Read(projectName) != null;

        public static Project CreateProject(IProjectRepository projectRepository, string projectName, IUser author)
        {
            var newId = GetNextId(projectRepository);
            var newProject = new Project(newId, projectName, author);
            if (projectRepository.Create(newProject))
            {
                JoinProject(projectRepository, projectName, author);
                return newProject;
            }
            throw new Exception($"Failed to create new project: {newProject}!");
        }

        public static bool JoinProject(IProjectRepository projectRepository, string projectName, IUser user)
        {
            var project = projectRepository.Read(projectName);

            if (project == null) return false;  // Project must exist
            if (project.ProjectMembers.Contains(user)) return false; // User may not already be in project

            project.ProjectMembers.Add(user);
            return projectRepository.Update(project);
        }

        private static int GetNextId(IProjectRepository projectRepository)
        {
            int i = 0;

            try
            {
                i = projectRepository.ReadAll().Keys.Max();
            }
            catch (InvalidOperationException) {}    // No projects available yet

            return i;
        }
    }
}
