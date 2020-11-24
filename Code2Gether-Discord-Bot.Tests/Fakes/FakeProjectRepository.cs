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
        public Dictionary<int, Project> Projects = new Dictionary<int, Project>();

        public bool Create(Project newProject)
        {
            return Projects.TryAdd(newProject.ID, newProject);
        }

        public Project Read(int id)
        {
            if (Projects.TryGetValue(id, out Project project))
                return project;
            throw new Exception($"Failed to read project with ID {id}");
        }

        public Project Read(string projectName)
        {
            return ReadAll().Values.FirstOrDefault(p => p.Name == projectName);
        }

        public IDictionary<int, Project> ReadAll()
        {
            return Projects;
        }

        public bool Update(Project newProject)
        {
            Delete(newProject.ID);
            return Create(newProject);
        }

        public bool Delete(int id)
        {
            return Projects.Remove(id, out _);
        }
    }
}
