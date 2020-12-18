using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.LeetModels;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class LeetLogic : IBusinessLogic
    {
        private readonly string _args;
        private readonly ICommandContext _context;
        private readonly ILogger _logger;

        public LeetLogic(ILogger logger, ICommandContext context, string args)
        {
            _logger = logger;
            _context = context;
            _args = args;
        }

        public async Task<Embed> ExecuteAsync()
        {
            _logger.Log(_context);

            var result = await GetQuestions(_args);

            // Set from a private method
            var title = result.Item1; // Title for leet question
            var description = result.Item2; // Prompt for leet question
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
            var formattedQues =
                questionTitle
                    .Replace("-", "") // Remove existing dashes
                    .Replace(" ", "-") // Changes spaces to dashes, to be sent in the url.
                    .ToLower();
            var values = new JsonRequest(formattedQues);
            var json = JsonConvert.SerializeObject(values);
            var plainDescription = "You requested an invalid question";
            var title = "Invalid";
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            const string url = "https://leetcode.com/graphql";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            var result = await response.Content.ReadAsStringAsync();
            var desJson = JsonConvert.DeserializeObject<JsonResponse>(result);
            var tagRemover = new Regex(@"<[^>]*>"); // Using Regex to remove HTML tags in JSON response.
            if (desJson.Data?.Question != null)
            {
                title = desJson.Data.Question.Title;
                plainDescription = $"*{desJson.Data.Question.TopicTags[0].Name}* \n{desJson.Data.Question.Content}";
                plainDescription = tagRemover
                    .Replace(plainDescription, "")
                    .Replace("&nbsp;", "");
            }

            return (title, plainDescription);
        }
    }
}