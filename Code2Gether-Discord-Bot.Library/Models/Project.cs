using Discord;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public class Project
    {
        [Key]
        public long ID { get; }
        [Required]
        public string Name { get; }
        [Required]
        public IUser Author { get; }
        public IList<IUser> ProjectMembers { get; set; } = new List<IUser>();
        public bool IsActive
        {
            get
            {
                // return ProjectMembers.Count > 2;
                return false;
            }
            }

        public Project() 
        {
            ProjectMembers = new List<IUser>();
        }

        public Project(long id, string name, IUser author) : this()
        {
            ID = id;
            Name = name;
            Author = author;
        }

        public override string ToString() =>
            $"Project Name: {Name}; Is Active: {IsActive}; Created by: {Author}"; // Members: {ProjectMembers.Count}";
    }
}
