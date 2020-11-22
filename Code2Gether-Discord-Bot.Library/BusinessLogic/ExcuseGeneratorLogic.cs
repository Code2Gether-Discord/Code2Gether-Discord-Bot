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
    public class ExcuseGeneratorLogic : IBusinessLogic
    {
        #region Fields
        private ILogger _logger;
        private ICommandContext _context;
        #endregion

        #region Constructor
        public ExcuseGeneratorLogic(ILogger logger, ICommandContext context)
        {
            _logger = logger;
            _context = context;
        }
        #endregion

        public Embed Execute()
        {
            _logger.Log(_context);

            Embed embed;

            try
            {
                if (!_context.Guild.GetCurrentUserAsync().Result.GuildPermissions.ManageMessages)
                {
                    throw new Exception("Missing ManageMessages permission for Message Latency");
                }

                var temporaryMessage = _context.Channel.SendMessageAsync("https://tenor.com/view/loading-buffering-gif-8820437").Result;

                embed = new EmbedBuilder()
                    .WithColor(Color.DarkBlue)
                    .WithTitle("Greetings. My excuse today is:")
                    .WithDescription("I just don't feel like it.")
                    .WithAuthor(_context.Message.Author)
                    .Build();

                temporaryMessage.DeleteAsync().Wait();
            }
            catch (Exception e)
            {
                _logger.Log(LogSeverity.Error, e);

                embed = new EmbedBuilder()
                    .WithColor(Color.DarkBlue)
                    .WithTitle("Greetings. My excuse today is:")
                    .WithDescription("I'm not working.")
                    .WithAuthor(_context.Message.Author)
                    .Build();
            }

            return embed;
        }

        public async Task<string> generateExcuseAsync()
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
                .Split(new string[] { prefixString, suffixString }, StringSplitOptions.None);

            // The "excuse" should always be the fourth item in the split.
            return split[3]; 
        }
    }
}
