using Code2Gether_Discord_Bot.Static;
using Discord.Commands;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Modules.UserModules
{
    public class ExcuseModule : ModuleBase<SocketCommandContext>
    {
        [Command("get_excuse", RunMode = RunMode.Async)]
        [Alias("getexcuse")]
        [Summary("Gets a random excuse.")]
        public async Task GetExcuseAsync() =>
            await ReplyAsync(embed: BusinessLogicFactory.ExcuseGeneratorLogic(GetType(), Context).Execute());
    }
}
