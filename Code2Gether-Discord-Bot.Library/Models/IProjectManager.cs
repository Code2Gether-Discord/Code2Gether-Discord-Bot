using System;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public interface IProjectManager
    {
        bool DoesProjectExist(string projectName);
        bool DoesProjectExist(string projectName, out Project project);
        Project CreateProject(string projectName, Member author);
        bool JoinProject(string projectName, Member user, out Project project);
    }
}
