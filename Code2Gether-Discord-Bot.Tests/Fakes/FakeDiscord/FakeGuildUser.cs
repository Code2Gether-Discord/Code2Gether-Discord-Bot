using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace Code2Gether_Discord_Bot.Tests.Fakes.FakeDiscord
{
    public class FakeGuildUser : IGuildUser
    {
        public FakeGuildUser(ulong id)
        {
            Id = id;
        }

        public ulong Id { get; }
        public DateTimeOffset CreatedAt { get; }
        public string Mention { get; }
        public IActivity Activity { get; }
        public UserStatus Status { get; }
        public IImmutableSet<ClientType> ActiveClients { get; }
        public string GetAvatarUrl(ImageFormat format = ImageFormat.Auto, ushort size = 128)
        {
            throw new NotImplementedException();
        }

        public string GetDefaultAvatarUrl()
        {
            throw new NotImplementedException();
        }

        public Task<IDMChannel> GetOrCreateDMChannelAsync(RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public string AvatarId { get; }
        public string Discriminator { get; }
        public ushort DiscriminatorValue { get; }
        public bool IsBot { get; }
        public bool IsWebhook { get; }
        public string Username { get; }
        public bool IsDeafened { get; }
        public bool IsMuted { get; }
        public bool IsSelfDeafened { get; }
        public bool IsSelfMuted { get; }
        public bool IsSuppressed { get; }
        public IVoiceChannel VoiceChannel { get; }
        public string VoiceSessionId { get; }
        public bool IsStreaming { get; }
        public ChannelPermissions GetPermissions(IGuildChannel channel)
        {
            throw new NotImplementedException();
        }

        public Task KickAsync(string reason = null, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task ModifyAsync(Action<GuildUserProperties> func, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task AddRoleAsync(IRole role, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task AddRolesAsync(IEnumerable<IRole> roles, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRoleAsync(IRole role, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRolesAsync(IEnumerable<IRole> roles, RequestOptions options = null)
        {
            throw new NotImplementedException();
        }

        public DateTimeOffset? JoinedAt { get; }
        public string Nickname { get; }
        public GuildPermissions GuildPermissions { get; }
        public IGuild Guild { get; }
        public ulong GuildId { get; }
        public DateTimeOffset? PremiumSince { get; }
        public IReadOnlyCollection<ulong> RoleIds { get; }
    }
}
