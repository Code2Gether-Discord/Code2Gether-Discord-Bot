using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository;

namespace Code2Gether_Discord_Bot.Tests.Fakes
{
    internal class FakeProjectRepository : IProjectRepository
    {
        Dictionary<int, Project> _projects = new Dictionary<int, Project>();

        public bool Create(Project newProject)
        {
            return _projects.TryAdd(newProject.ID, newProject);
        }

        public Project Read(int id)
        {
            if (_projects.TryGetValue(id, out Project project))
                return project;
            throw new Exception($"Failed to read project with ID {id}");
        }

        public Project Read(string projectName)
        {
            return ReadAll().Values.FirstOrDefault(p => p.Name == projectName);
        }

        public IDictionary<int, Project> ReadAll()
        {
            return _projects;
        }

        public bool Update(Project newProject)
        {
            Delete(newProject.ID);
            return Create(newProject);
        }

        public bool Delete(int id)
        {
            return _projects.Remove(id, out _);
        }
    }
}
