using Code2Gether_Discord_Bot.Library.Models;
using Discord;
using Discord.Commands;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class ExcuseGeneratorLogic : BaseLogic
    {
        #region Constructor
        public ExcuseGeneratorLogic(ILogger logger, ICommandContext context) : base(logger, context)
        {
        }
        #endregion

        public override async Task<Embed> ExecuteAsync()
        {
            var response = await GenerateExcuseAsync();

            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle("Greetings. My excuse today is:")
                .WithDescription(response) // Excuse generation logic occurs here.
                .WithAuthor(_context.Message.Author)
                .Build();
            return embed;
        }

        private async Task<string> GenerateExcuseAsync()
        {
            try
            {
                // Get HTML from website
                var response = await new HttpClient()
                    .GetByteArrayAsync("http://pages.cs.wisc.edu/~ballard/bofh/bofhserver.pl");

                // Encode byte array to string, remove line breaks.
                var decodedSource = WebUtility.HtmlDecode(Encoding.UTF8.GetString(response, 0, response.Length - 1))
                    .Replace("\n", "")
                    .Replace("\r", "");

                // Isolate the "Excuse" by finding its prefix and suffix HTML tags
                var prefixString = @"""+2"">";
                var suffixString = @"</font>";

                var split = decodedSource
                    .Split(new[] { prefixString, suffixString }, StringSplitOptions.None);

                // The "excuse" should always be the fourth item in the split.
                return split[3];
            }
            catch(Exception e)
            {
                _logger.Log(LogSeverity.Error, e.Message);
                return "Because I'm not working";
            }
        }
    }
}
