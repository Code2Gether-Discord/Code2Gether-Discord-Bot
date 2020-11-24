using Discord;
using SQLite;
using System.Collections.Generic;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public class Project
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; }
        public string Name { get; }
        public List<IUser> ProjectMembers { get; set; } = new List<IUser>();
        public bool IsActive
        {
            get
            {
                return ProjectMembers.Count > 2;
            }
        }

        public Project(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public override string ToString() =>
            $"ID: {ID}; Name: {Name}; Members: {ProjectMembers.Count}";
    }
}
