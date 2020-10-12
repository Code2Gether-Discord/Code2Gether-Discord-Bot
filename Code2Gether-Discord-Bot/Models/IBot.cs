using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Models
{
    public interface IBot
    {
        Task RunAsync();
        Task RegisterCommandsAsync();
    }
}
