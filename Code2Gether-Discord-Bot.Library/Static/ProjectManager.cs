using System;
using System.Linq;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository;
using SQLitePCL;

namespace Code2Gether_Discord_Bot.Library.Static
{
    public static class ProjectManager
    {
        public static bool DoesProjectExist(IProjectRepository projectRepository, string projectName) =>
            projectRepository.Read(projectName) != null;

        public static Project CreateProject(IProjectRepository projectRepository, string projectName)
        {
            var newId = GetNextId(projectRepository);
            var newProject = new Project(newId, projectName);
            if (projectRepository.Create(newProject))
            {
                return newProject;
            }
            throw new Exception($"Failed to create new project: {newProject}!");
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
