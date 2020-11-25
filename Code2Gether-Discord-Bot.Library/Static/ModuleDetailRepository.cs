using Discord.Commands;
using System.Collections.Generic;

namespace Code2Gether_Discord_Bot.Library.Static
{
    public static class ModuleDetailRepository
    {
        public static IEnumerable<ModuleInfo> Modules { get; set; }
    }
}
