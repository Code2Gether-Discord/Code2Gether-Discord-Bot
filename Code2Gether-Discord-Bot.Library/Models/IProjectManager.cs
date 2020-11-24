using System;
using System.Collections.Generic;
using System.Text;
using Discord;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public interface IProjectManager
    {
        bool DoesProjectExist(string projectName);
        Project CreateProject(string projectName, IUser author);
        bool JoinProject(string projectName, IUser user, out Project project);
    }
}
