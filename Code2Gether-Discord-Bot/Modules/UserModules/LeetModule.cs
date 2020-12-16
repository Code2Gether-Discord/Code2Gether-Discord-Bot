using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Static;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Modules.UserModules
{
    public class LeetModule : ModuleBase<SocketCommandContext>
    {
        [Command("leet", 
            RunMode = RunMode.Async)]
        //[Alias()]
        [Summary("Reply with a question from leetcode.com with a given title.")]
        public async Task LeetAsync([Remainder] string title = "") =>
            await ReplyAsync(embed: await BusinessLogicFactory.GetLeetLogic(GetType(), Context, title).ExecuteAsync());

        [Command("leetans",
            RunMode = RunMode.Async)]
        [Alias("leetanswer")]
        [Summary("Provide an answer for an open leet question.")]
        public async Task LeetAnsAsync([Remainder] string answer = "") =>
            await ReplyAsync(embed: await BusinessLogicFactory.GetLeetAnsLogic(GetType(), Context, answer).ExecuteAsync());
    }
}
