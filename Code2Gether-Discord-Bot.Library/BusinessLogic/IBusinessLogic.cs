using Discord;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public interface IBusinessLogic
    {
        Task<Embed> ExecuteAsync();
    }
}