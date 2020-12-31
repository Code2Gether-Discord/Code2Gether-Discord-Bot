using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.Repositories;

namespace Code2Gether_Discord_Bot.Tests.Fakes.FakeRepositories
{
    public class FakeProjectRepository : IProjectRepository
    {
        public IDictionary<int, Project> Projects = new ConcurrentDictionary<int, Project>();

        public Task<bool> CreateAsync(Project newProject)
        {
            return Task.FromResult(Projects.TryAdd(newProject.ID, newProject));
        }

        public Task<Project> ReadAsync(int id)
        {
            if (Projects.TryGetValue(id, out Project project))
                return Task.FromResult(project);

            throw new Exception($"Failed to read project with ID {id}");
        }

        public Task<Project> ReadAsync(string projectName)
        {
            return Task.FromResult(Projects
                .Values
                .FirstOrDefault(p => p.Name == projectName));
        }

        public Task<IEnumerable<Project>> ReadAllAsync()
        {
            return Task.FromResult(Projects.Values.AsEnumerable());
        }

        public async Task<bool> UpdateAsync(Project existingProject)
        {
            if (await DeleteAsync(existingProject.ID))
                return Projects.TryAdd(existingProject.ID, existingProject);
            return false;
        }

        public Task<bool> DeleteAsync(int id)
        {
            return Task.FromResult(Projects.Remove(id, out _));
        }

        public Task<bool> AddMemberAsync(Project project, Member member)
        {
            if (!project.Members.Any(m => m.SnowflakeId == member.SnowflakeId))
                project.Members.Add(member);

            return Task.FromResult(project.Members.Any(m => m.SnowflakeId == member.SnowflakeId));
        }

        public Task<bool> RemoveMemberAsync(Project project, Member member)
        {
            throw new NotImplementedException();
        }
    }
}
