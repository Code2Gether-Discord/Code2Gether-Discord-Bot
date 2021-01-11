using Newtonsoft.Json;

namespace Code2Gether_Discord_Bot.Models
{
    public interface IConfig
    {
        string DiscordToken { get; set; }
        string Prefix { get; set; }
        bool Debug { get; set; }
        string ConnectionString { get; set; }
        string GitHubAuthToken { get; set; }
        string GitHubOrganizationName { get; set; }
    }

    public class Config : IConfig
    {
        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("token")]
        public string DiscordToken { get; set; }

        [JsonProperty("debug")]
        public bool Debug { get; set; }

        [JsonProperty("connectionString")]
        public string ConnectionString { get; set; }

        [JsonProperty("githubAuthToken")]
        public string GitHubAuthToken { get; set; }

        [JsonProperty("githubOrganizationName")]
        public string GitHubOrganizationName { get; set; }

        public Config() { }

        public Config(string prefix, string token, bool debug, string connectionString, string gitHubAuthToken, string githubOrganizationName)
        {
            Prefix = prefix;
            DiscordToken = token;
            Debug = debug;
            ConnectionString = connectionString;
            GitHubAuthToken = gitHubAuthToken;
            GitHubOrganizationName = githubOrganizationName;
        }
    }
}
