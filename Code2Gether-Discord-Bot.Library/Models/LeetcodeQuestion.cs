using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Code2Gether_Discord_Bot.Library.Models
{
    class LeetcodeQuestion
    {
        public class JSONRequest
        {
            [JsonProperty("operationName")]
            public string OperationName = "questionData";
            [JsonProperty("variables")]

            public Variables Variables = new Variables();
            [JsonProperty("query")]

            public string Query = "query questionData($titleSlug: String!) {\n  question(titleSlug: $titleSlug) {\n    questionId\n    questionFrontendId\n    boundTopicId\n    title\n    titleSlug\n    content\n    translatedTitle\n    translatedContent\n    isPaidOnly\n    difficulty\n    likes\n    dislikes\n    isLiked\n    similarQuestions\n    contributors {\n      username\n      profileUrl\n      avatarUrl\n      __typename\n    }\n    topicTags {\n      name\n      slug\n      translatedName\n      __typename\n    }\n    companyTagStats\n    codeSnippets {\n      lang\n      langSlug\n      code\n      __typename\n    }\n    stats\n    hints\n    solution {\n      id\n      canSeeDetail\n      paidOnly\n      hasVideoSolution\n      __typename\n    }\n    status\n    sampleTestCase\n    metaData\n    judgerAvailable\n    judgeType\n    mysqlSchemas\n    enableRunCode\n    enableTestMode\n    enableDebugger\n    envInfo\n    libraryUrl\n    adminUrl\n    __typename\n  }\n}\n";
            public JSONRequest(string qName)
            {
                Variables.TitleSlug = qName;
            }
        }

        public class Variables
        {
            [JsonProperty("titleSlug")]

            public string TitleSlug;

        }
        public partial class JSONResponse
        {
            [JsonProperty("data")]
            public Data Data { get; set; }
        }

        public partial class Data
        {
            [JsonProperty("question")]
            public Question Question { get; set; }
        }

        public partial class Question
        {
      

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("titleSlug")]
            public string TitleSlug { get; set; }

            [JsonProperty("content")]
            public string Content { get; set; }
            [JsonProperty("topicTags")]
            public Topics[] TopicTags { get; set; }

        }
        public partial class Topics
        {
            [JsonProperty("name")]

            public string Name { get; set; }
        }
    }
}
