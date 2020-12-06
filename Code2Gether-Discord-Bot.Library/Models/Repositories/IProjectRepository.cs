using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Library.Models.Repositories
{
    public interface IProjectRepository : IDataRepository<Project>
    {
        Task<Project> ReadAsync(string projectName);
    }
}
