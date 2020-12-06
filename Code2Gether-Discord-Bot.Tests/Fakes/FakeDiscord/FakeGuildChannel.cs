using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;

namespace Code2Gether_Discord_Bot.Tests.Fakes.FakeDiscord
{
    internal class FakeGuildChannel : IGuildChannel
    {
        public ulong Id { get; }
        public DateTimeOffset CreatedAt { get; }
        public Task ModifyAsync(Action<GuildChannelProperties> func, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public OverwritePermissions? GetPermissionOverwrite(IRole role)
        {
            throw new NotImplementedException();
        }

        public OverwritePermissions? GetPermissionOverwrite(IUser user)
        {
            throw new NotImplementedException();
        }

        public Task RemovePermissionOverwriteAsync(IRole role, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task RemovePermissionOverwriteAsync(IUser user, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task AddPermissionOverwriteAsync(IRole role, OverwritePermissions permissions, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task AddPermissionOverwriteAsync(IUser user, OverwritePermissions permissions, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<IReadOnlyCollection<IGuildUser>> GetUsersAsync(CacheMode mode = CacheMode.AllowDownload, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        Task<IGuildUser> IGuildChannel.GetUserAsync(ulong id, CacheMode mode, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public int Position { get; }
        public IGuild Guild { get; }
        public ulong GuildId { get; }
        public IReadOnlyCollection<Overwrite> PermissionOverwrites { get; }

        IAsyncEnumerable<IReadOnlyCollection<IUser>> IChannel.GetUsersAsync(CacheMode mode, RequestOptions options)
        {
            return GetUsersAsync(mode, options);
        }

        Task<IUser> IChannel.GetUserAsync(ulong id, CacheMode mode, RequestOptions options)
        {
            throw new NotImplementedException();
        }

        public string Name { get; }
        public Task DeleteAsync(RequestOptions options = null)
        {
            throw new NotImplementedException();
        }
    }
}
