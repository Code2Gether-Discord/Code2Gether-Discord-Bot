using System.Collections.Generic;

namespace Code2Gether_Discord_Bot.Library.Models.Repositories.ProjectRepository
{
    public interface IProjectRepository
    {
        bool Create(Project newProject);
        Project Read(long id);
        Project Read(string projectName);
        IDictionary<long, Project> ReadAll();
        bool Update(Project newProject);
        bool Delete(long id);
    }
}