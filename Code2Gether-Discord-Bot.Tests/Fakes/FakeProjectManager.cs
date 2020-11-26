using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository;
using Discord;

namespace Code2Gether_Discord_Bot.Tests.Fakes
{
    class FakeProjectManager : IProjectManager
    {
        private IProjectRepository _projectRepository;

        public FakeProjectManager(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public bool DoesProjectExist(string projectName) =>
            _projectRepository.Read(projectName) != null;

        public bool DoesProjectExist(string projectName, out Project project)
        {
            project = _projectRepository.Read(projectName);
            return project != null;
        }

        public Project CreateProject(string projectName, User author)
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

        public bool JoinProject(string projectName, User user, out Project project)
        {
            project = _projectRepository.Read(projectName);

            if (project == null) return false;  // Project must exist
            if (project.ProjectMembers.Contains(user)) return false; // User may not already be in project

            project.ProjectMembers.Add(user);

            return _projectRepository.Update(project);
        }

        private long GetNextId()
        {
            long i = 0;

            try
            {
                i = _projectRepository.ReadAll().Keys.Max();
            }
            catch (InvalidOperationException) { }    // No projects available yet

            return i;
        }
    }
}
