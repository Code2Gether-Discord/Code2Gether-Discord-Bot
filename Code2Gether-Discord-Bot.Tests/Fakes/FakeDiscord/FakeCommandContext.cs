using Discord;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Tests.Fakes
{
    internal class FakeCommandContext : ICommandContext
    {
        public IDiscordClient Client { get; set; }

        public IGuild Guild { get; set; }

        public IMessageChannel Channel { get; set; }

        public IUser User { get; set; }

        public IUserMessage Message { get; set; }
    }
}
