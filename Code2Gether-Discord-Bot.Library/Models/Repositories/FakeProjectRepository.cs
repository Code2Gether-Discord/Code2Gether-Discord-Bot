using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Library.Models.Repositories
{
    public class FakeProjectRepository : IDataRepository<Project>
    {
        private ConcurrentDictionary<int, Project> _projects = new ConcurrentDictionary<int, Project>();

        public Task<bool> CreateAsync(Project newProject)
        {
            return Task.FromResult(_projects.TryAdd(newProject.ID, newProject));
        }

        public Task<Project> ReadAsync(int id)
        {
            if (_projects.TryGetValue(id, out Project project))
                return Task.FromResult(project);

            throw new Exception($"Failed to read project with ID {id}");
        }

        public Task<Project> ReadAsync(string projectName)
        {
            return Task.FromResult(_projects
                .Values
                .FirstOrDefault(p => p.Name == projectName));
        }

        public Task<IEnumerable<Project>> ReadAllAsync()
        {
            return Task.FromResult(_projects.Values.AsEnumerable());
        }

        public async Task<bool> UpdateAsync(Project newProject)
        {
            return _projects.TryUpdate(newProject.ID, newProject, await ReadAsync(newProject.ID));
        }

        public Task<bool> DeleteAsync(int id)
        {
            return Task.FromResult(_projects.TryRemove(id, out _));
        }
    }
}
