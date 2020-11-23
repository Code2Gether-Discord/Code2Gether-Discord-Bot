using Code2Gether_Discord_Bot.Static;
using Discord.Commands;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Modules.UserModules
{
    public class CreateProjectModule : ModuleBase<SocketCommandContext>
    {
        [Command("createproject",
            RunMode = RunMode.Async)]
        [Alias("create")]
        [Summary("Creates an inactive project that other users can join."
            + "Upon becomming active, a new text channel is created and project members gain the project's role")]
        public async Task CreateProjectAsync([Remainder] string arguments) =>
             await ReplyAsync(embed: BusinessLogicFactory.GetCreateProjectLogic(GetType(), Context, arguments).Execute());
    }
}
