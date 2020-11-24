using System;
using System.Linq;
using Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository;
using Discord;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public class ProjectManager : IProjectManager
    {
        private IProjectRepository _projectRepository;

        public ProjectManager(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public bool DoesProjectExist(string projectName) =>
            _projectRepository.Read(projectName) != null;

        public Project CreateProject(string projectName, IUser author)
        {
            var newId = GetNextId();
            var newProject = new Project(newId, projectName, author);
            if (_projectRepository.Create(newProject))
            {
                JoinProject(projectName, author, out newProject);
                return newProject;
            }
            throw new Exception($"Failed to create new project: {newProject}!");
        }

        public bool JoinProject(string projectName, IUser user, out Project project)
        {
            project = _projectRepository.Read(projectName);

            if (project == null) return false;  // Project must exist
            if (project.ProjectMembers.Contains(user)) return false; // User may not already be in project

            project.ProjectMembers.Add(user);

            if (project.IsActive) TransitionInactiveToActiveProject();

            return _projectRepository.Update(project);
        }

        private void TransitionInactiveToActiveProject()
        {
            throw new NotImplementedException();
        }

        private int GetNextId()
        {
            int i = 0;

            try
            {
                i = _projectRepository.ReadAll().Keys.Max();
            }
            catch (InvalidOperationException) {}    // No projects available yet

            return i;
        }
    }
}
