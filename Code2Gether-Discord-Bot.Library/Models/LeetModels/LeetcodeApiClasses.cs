using Newtonsoft.Json;

namespace Code2Gether_Discord_Bot.Library.Models.LeetModels
{
    public class JsonRequest
    {
        [JsonProperty("operationName")] public string OperationName = "questionData";

        [JsonProperty("query")] public string Query =
            "query questionData($titleSlug: String!) {\n  question(titleSlug: $titleSlug) {\n    questionId\n    questionFrontendId\n    boundTopicId\n    title\n    titleSlug\n    content\n    translatedTitle\n    translatedContent\n    isPaidOnly\n    difficulty\n    likes\n    dislikes\n    isLiked\n    similarQuestions\n    contributors {\n      username\n      profileUrl\n      avatarUrl\n      __typename\n    }\n    topicTags {\n      name\n      slug\n      translatedName\n      __typename\n    }\n    companyTagStats\n    codeSnippets {\n      lang\n      langSlug\n      code\n      __typename\n    }\n    stats\n    hints\n    solution {\n      id\n      canSeeDetail\n      paidOnly\n      hasVideoSolution\n      __typename\n    }\n    status\n    sampleTestCase\n    metaData\n    judgerAvailable\n    judgeType\n    mysqlSchemas\n    enableRunCode\n    enableTestMode\n    enableDebugger\n    envInfo\n    libraryUrl\n    adminUrl\n    __typename\n  }\n}\n";

        [JsonProperty("variables")] public Variables Variables = new Variables();

        public JsonRequest(string questionTitle)
        {
            Variables.TitleSlug = questionTitle;
        }
    }

    public class Variables
    {
        [JsonProperty("titleSlug")] public string TitleSlug;
    }

    public class JsonResponse
    {
        [JsonProperty("data")] public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("question")] public Question Question { get; set; }
    }

    public class Question
    {
        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("titleSlug")] public string TitleSlug { get; set; }

        [JsonProperty("content")] public string Content { get; set; }

        [JsonProperty("topicTags")] public Topics[] TopicTags { get; set; }
    }

    public class Topics
    {
        [JsonProperty("name")] public string Name { get; set; }
    }
}