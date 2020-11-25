using Discord;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public interface IBusinessLogic
    {
        // This is a potentially long-running task, async should be used if necessary.
        // Convert this to Task<Embed> ExecuteAsync()
        Embed Execute();
    }
}