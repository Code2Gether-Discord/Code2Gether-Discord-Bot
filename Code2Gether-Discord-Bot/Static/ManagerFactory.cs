using System;
using System.Collections.Generic;
using System.Text;
using Code2Gether_Discord_Bot.Library.Models;
using Code2Gether_Discord_Bot.Library.Static;

namespace Code2Gether_Discord_Bot.Static
{
    public class ManagerFactory
    {
        public static IProjectManager GetProjectManager() =>
            new ProjectManager(RepositoryFactory.GetProjectRepository());
    }
}
