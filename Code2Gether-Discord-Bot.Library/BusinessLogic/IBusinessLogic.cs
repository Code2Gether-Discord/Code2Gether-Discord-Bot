using System.Threading.Tasks;
using Discord;

namespace Code2Gether_Discord_Bot.Library.BusinessLogic
{
    public interface IBusinessLogic
    {
        Task<Embed> Execute();
    }
}