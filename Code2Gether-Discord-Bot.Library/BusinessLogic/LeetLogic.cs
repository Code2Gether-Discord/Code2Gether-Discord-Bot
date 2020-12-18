using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
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


            var result = await GetQuestions(_args.Replace("-", "").ToLower());
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
                questionTitle.Replace(" ", "-").ToLower(); //Changes spaces to dashes, to be sent in the url.
            var values = new JSONRequest(formattedQues);
            var json = JsonConvert.SerializeObject(values);
            var plainDescription = "You requested an invalid question";
            var title = "Invalid";
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            const string url = "https://leetcode.com/graphql";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            var result = await response.Content.ReadAsStringAsync();
            var desJson = JsonConvert.DeserializeObject<JSONResponse>(result);
            var tagRemover = new Regex(@"<[^>]*>"); //using Regex to remove HTML tags in JSON response.
            if (desJson.data.question != null)
            {
                title = desJson.data.question.title;
                plainDescription = desJson.data.question.content;
                plainDescription = tagRemover.Replace(plainDescription, "").Replace("&nbsp;", "");
            }

            return (title, plainDescription);
        }

        public class JSONRequest
        {
            public string operationName = "questionData";

            public string query =
                "query questionData($titleSlug: String!) {\n  question(titleSlug: $titleSlug) {\n    questionId\n    questionFrontendId\n    boundTopicId\n    title\n    titleSlug\n    content\n    translatedTitle\n    translatedContent\n    isPaidOnly\n    difficulty\n    likes\n    dislikes\n    isLiked\n    similarQuestions\n    contributors {\n      username\n      profileUrl\n      avatarUrl\n      __typename\n    }\n    topicTags {\n      name\n      slug\n      translatedName\n      __typename\n    }\n    companyTagStats\n    codeSnippets {\n      lang\n      langSlug\n      code\n      __typename\n    }\n    stats\n    hints\n    solution {\n      id\n      canSeeDetail\n      paidOnly\n      hasVideoSolution\n      __typename\n    }\n    status\n    sampleTestCase\n    metaData\n    judgerAvailable\n    judgeType\n    mysqlSchemas\n    enableRunCode\n    enableTestMode\n    enableDebugger\n    envInfo\n    libraryUrl\n    adminUrl\n    __typename\n  }\n}\n";

            public Variables variables = new Variables();

            public JSONRequest(string qName)
            {
                variables.titleSlug = qName;
            }
        }

        public class Variables
        {
            public string titleSlug;
        }

        public class JSONResponse
        {
            public DataJSONResponse data = new DataJSONResponse();
        }

        public class DataJSONResponse
        {
            public QuestionJSONResponse question = new QuestionJSONResponse();
        }

        public class QuestionJSONResponse
        {
            public string title { get; set; }
            public string content { get; set; }
        }
    }
}