using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Models.CustomExceptions;
using Discord;
using Discord.Commands;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public class GitHubJoinLogic : BaseLogic
    {
        private const string GITHUB_ENDPOINT = "https://api.github.com/orgs/Code2Gether-Discord/invitations";
        private readonly string _githubAuthToken;
        private readonly string _userGitHubEmail;

        public GitHubJoinLogic(ILogger logger, ICommandContext context, string githubAuthToken, string args) : base(logger, context)
        {
            _githubAuthToken = githubAuthToken;
            _userGitHubEmail = args.Trim();

            if (!HttpHelper.IsValidEmail(_userGitHubEmail))
            {
                throw new InvalidEmailException($"{_userGitHubEmail} is not a valid email");
            }
        }
        
        public override async Task<Embed> ExecuteAsync()
        {
            var result = await JoinGitHubOrganization();

            var embed = new EmbedBuilder()
                .WithColor(Color.Purple)
                .WithTitle(result.Title)
                .WithDescription(result.Description)
                .WithAuthor(_context.Message.Author)
                .Build();

            return embed;
        }

        private async Task<EmbedContent> JoinGitHubOrganization()
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(
                MediaTypeWithQualityHeaderValue.Parse("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(_githubAuthToken);

            var content = new StringContent($"{{\"email\": \"{_userGitHubEmail}\"}}");

            var response = await client.PostAsync(GITHUB_ENDPOINT, content);
            
            return ParseHttpResponseToEmbedContent(response, _context.User.Mention);
        }

        public static EmbedContent ParseHttpResponseToEmbedContent(HttpResponseMessage response, string discordUserMention)
        {
            var embedContent = new EmbedContent
            {
                Title = "GitHub Join"
            };

            if (response.IsSuccessStatusCode)
            {
                embedContent.Title += "Successful";
                embedContent.Description = $"Welcome to the Organization {discordUserMention}!";
            }
            else
            {
                embedContent.Title += "Failed";
                embedContent.Description = $"There was an error requesting to join the organization! {response.ReasonPhrase}";
            }

            return embedContent;
        }
    }
}
