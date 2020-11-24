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
        public IUser Author { get; }
        public List<IUser> ProjectMembers { get; set; } = new List<IUser>();
        public bool IsActive
        {
            get
            {
                return ProjectMembers.Count > 2;
            }
        }

        public Project(int id, string name, IUser author)
        {
            ID = id;
            Name = name;
            Author = author;
        }

        public override string ToString() =>
            $"Project Name: {Name}; Is Active: {IsActive}; Created by: {Author}; Members: {ProjectMembers.Count}";
    }
}
