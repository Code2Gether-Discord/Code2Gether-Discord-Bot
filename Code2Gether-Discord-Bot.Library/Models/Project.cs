﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public class Project
    {
        #region properies
        [Key]
        public long ID { get; }
        [Required]
        public string Name { get; }
        [Required]
        public User Author { get; }
        public IList<User> ProjectMembers { get; set; } = new List<User>();
        [NotMapped]
        public bool IsActive => ProjectMembers.Count > 2;
        #endregion

        #region Constructors
        public Project()
        {
            ProjectMembers = new List<User>();
        }

        public Project(long id, string name, User author) : this()
        {
            ID = id;
            Name = name;
            Author = author;
        }
        #endregion

        #region Methods
        public override string ToString() =>
            $"Project Name: {Name}; Is Active: {IsActive}; Created by: {Author}"; // Members: {ProjectMembers.Count}";
        #endregion
    }
}
