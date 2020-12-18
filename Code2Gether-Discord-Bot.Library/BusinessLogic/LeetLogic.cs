using Code2Gether_Discord_Bot.Library.Models;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Code2Gether_Discord_Bot.Library.Models.LeetcodeQuestion;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class LeetLogic : IBusinessLogic
    {
        private ILogger _logger;
        private ICommandContext _context;
        private string _args;
        public LeetLogic(ILogger logger, ICommandContext context, string args)
        {
            _logger = logger;
            _context = context;
            _args = args;
        }
        public async Task<Embed> ExecuteAsync()
        {
            _logger.Log(_context);


            var result = await GetQuestions(_args.Replace("-", "").ToLower());
            // Set from a private method
            string title = result.Item1;    // Title for leet question
            string description = result.Item2;  // Prompt for leet question
            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle($"Leet Question: {title}")
                .WithDescription(description)
                .WithAuthor(_context.Message.Author)
                .Build();

            return await Task.FromResult(embed);
        }
        private async Task<(string, string)> GetQuestions(string questionTitle)
        {
            var formattedQues = questionTitle.Replace(" ", "-").ToLower(); //Changes spaces to dashes, to be sent in the url.
            var values = new JSONRequest(formattedQues);
            var json = JsonConvert.SerializeObject(values);
            string plainDescription = "You requested an invalid question";
            string title = "Invalid";
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            const string url = "https://leetcode.com/graphql";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            string result = await response.Content.ReadAsStringAsync();
            var desJson = JsonConvert.DeserializeObject<JSONResponse>(result);
            var tagRemover = new Regex(@"<[^>]*>"); //using Regex to remove HTML tags in JSON response.
            if (desJson.Data != null)
            {
                title = desJson.Data.Question.Title;
                plainDescription = $"*{desJson.Data.Question.TopicTags[0].Name}* \n" + desJson.Data.Question.Content;
                plainDescription = tagRemover.Replace(plainDescription, "").Replace("&nbsp;", "");
            }
            return (title, plainDescription);
        }

    }
}
