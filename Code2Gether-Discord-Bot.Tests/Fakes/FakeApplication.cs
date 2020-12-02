using Discord;
using System;

namespace Code2Gether_Discord_Bot.Tests.Fakes
{
    internal class FakeApplication : IApplication
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string[] RPCOrigins { get; set; }

        public ulong Flags { get; set; }

        public string IconUrl { get; set; }

        public IUser Owner { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public ulong Id { get; set; }
    }
}
