using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Library.Static
{
    public static class ParseCommandArguments
    {
        public static string[] ParseBy (char deliminator, string arguments) =>
            arguments.Trim().Split(deliminator);
    }
}
