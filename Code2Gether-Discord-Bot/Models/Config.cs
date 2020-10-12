using Newtonsoft.Json;

namespace Code2Gether_Discord_Bot.Models
{
    public interface IConfig
    {
        string DiscordToken { get; set; }
        string Prefix { get; set; }
    }

    public class Config : IConfig
    {
        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("token")]
        public string DiscordToken { get; set; }

        public Config() { }

        public Config(string prefix, string token)
        {
            Prefix = prefix;
            DiscordToken = token;
        }
    }
}
