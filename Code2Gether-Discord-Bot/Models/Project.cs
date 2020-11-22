using Discord;
using System;
using System.Collections.Generic;
using System.Text;

namespace Code2Gether_Discord_Bot.Models
{
    class Project
    {
        public string Name { get; }
        public List<IUser> ProjectMembers { get; set; } = new List<IUser>();
        public bool IsActive
        {
            get
            {
                return ProjectMembers.Count > 2;
            }
        }
        public Project(string name)
        {
            Name = name;
        }

    }
}
