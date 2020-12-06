using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Library.Models.Repositories
{
    internal class FakeMemberRepository : IDataRepository<Member>
    {
        private ConcurrentDictionary<int, Member> _projects = new ConcurrentDictionary<int, Member>();

        public Task<bool> CreateAsync(Member newProject)
        {
            return Task.FromResult(_projects.TryAdd(newProject.ID, newProject));
        }

        public Task<Member> ReadAsync(int id)
        {
            if (_projects.TryGetValue(id, out Member project))
                return Task.FromResult(project);

            throw new Exception($"Failed to member project with ID {id}");
        }

        public Task<Member> ReadAsync(ulong memberSnowflakeId)
        {
            return Task.FromResult(_projects.Values.FirstOrDefault(p => p.SnowflakeId == memberSnowflakeId));
        }

        public Task<IEnumerable<Member>> ReadAllAsync()
        {
            return Task.FromResult(_projects.Select(x => x.Value));
        }

        public async Task<bool> UpdateAsync(Member newProject)
        {
            return _projects.TryUpdate(newProject.ID, newProject, await ReadAsync(newProject.ID));
        }

        public Task<bool> DeleteAsync(int id)
        {
            return Task.FromResult(_projects.TryRemove(id, out _));
        }
    }
}
