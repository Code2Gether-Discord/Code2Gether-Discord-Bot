using System.Collections.Generic;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Library.Models.Repositories
{
    public interface IDataRepository<TModel>
    {
        Task<bool> CreateAsync(TModel newModel);
        Task<TModel> ReadAsync(int id);
        Task<IEnumerable<TModel>> ReadAllAsync();
        Task<bool> UpdateAsync(TModel existingModel);
        Task<bool> DeleteAsync(int id);
    }
}