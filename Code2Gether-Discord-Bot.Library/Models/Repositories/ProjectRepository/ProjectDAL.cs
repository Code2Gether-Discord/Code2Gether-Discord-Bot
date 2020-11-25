using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository
{
    /// <summary>
    /// TODO: replace with actual database access implementation
    /// </summary>
    public class ProjectDAL : IProjectRepository
    {
        private ConcurrentDictionary<long, Project> _projects = new ConcurrentDictionary<long, Project>();

        public bool Create(Project newProject)
        {
            return _projects.TryAdd(newProject.ID, newProject);
        }

        public Project Read(long id)
        {
            if (_projects.TryGetValue(id, out Project project))
                return project;
            throw new Exception($"Failed to read project with ID {id}");
        }

        public Project Read(string projectName)
        {
            return ReadAll().Values.FirstOrDefault(p => p.Name == projectName);
        }

        public IDictionary<long, Project> ReadAll()
        {
            return _projects;
        }

        public bool Update(Project newProject)
        {
            return _projects.TryUpdate(newProject.ID, newProject, Read(newProject.ID));
        }

        public bool Delete(long id)
        {
            return _projects.TryRemove(id, out _);
        }
    }
}
