using Discord;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Tests.Fakes
{
    internal class FakeUser : IUser
    {
        public string AvatarId { get; set; }

        public string Discriminator { get; set; }

        public ushort DiscriminatorValue { get; set; }

        public bool IsBot { get; set; }

        public bool IsWebhook { get; set; }

        public string Username { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public ulong Id { get; set; }

        public string Mention { get; set; }

        public IActivity Activity { get; set; }

        public UserStatus Status { get; set; }

        public IImmutableSet<ClientType> ActiveClients { get; set; }

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
    }
}
