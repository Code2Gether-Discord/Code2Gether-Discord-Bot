using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.Repositories;

namespace Code2Gether_Discord_Bot.Tests.Fakes.FakeRepositories
{
    internal class FakeMemberRepository : IMemberRepository
    {
        public IDictionary<int, Member> Members = new ConcurrentDictionary<int, Member>();

        public Task<bool> CreateAsync(Member newMember)
        {
            return Task.FromResult(Members.TryAdd(newMember.ID, newMember));
        }

        public Task<Member> ReadAsync(int id)
        {
            if (Members.TryGetValue(id, out Member member))
                return Task.FromResult(member);

            throw new Exception($"Failed to member project with ID {id}");
        }

        public Task<Member> ReadFromSnowflakeAsync(ulong memberSnowflakeId)
        {
            return Task.FromResult(Members.Values.FirstOrDefault(p => p.SnowflakeId == memberSnowflakeId));
        }

        public Task<IEnumerable<Member>> ReadAllAsync()
        {
            return Task.FromResult(Members.Select(x => x.Value));
        }

        public async Task<bool> UpdateAsync(Member existingMember)
        {
            if (await DeleteAsync(existingMember.ID))
                return Members.TryAdd(existingMember.ID, existingMember);
            return false;
        }

        public Task<bool> DeleteAsync(int id)
        {
            return Task.FromResult(Members.Remove(id, out _));
        }
    }
}
