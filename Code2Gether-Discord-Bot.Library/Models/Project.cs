using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public class Project
    {
        #region Fields
        private readonly ILazyLoader _lazyLoader;
        private User _author;
        #endregion

        #region Properies
        [Key]
        public long ID { get; set; }
        public string Name { get; set; }
        public long AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public User Author 
        { 
            get => _lazyLoader.Load(this, ref _author);
            set
            {
                _author = value;
                AuthorId = _author.ID;
            }
        }
        public ICollection<User> ProjectMembers { get; set; }
        [NotMapped]
        [JsonIgnore]
        public bool IsActive => ProjectMembers.Count > 2;
        #endregion

        #region Constructors
        public Project()
        {
            ProjectMembers = new List<User>();
        }

        public Project(ILazyLoader lazyLoader) : this()
        {
            _lazyLoader = lazyLoader;
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
