using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Code2Gether_Discord_Bot.Library.Interfaces;

namespace Code2Gether_Discord_Bot.Library.Models
{
    [Table("PROJECT")]
    public class Project : IProject
    {
        #region Fields
        private readonly ILazyLoader _lazyLoader;
        private Member _author;
        #endregion

        #region Properies
        [Column("PROJECT_ID")]
        [Key]
        public long ID { get; set; }
        [Column("PROJECT_NAME")]
        public string Name { get; set; }
        [Column("MEMBER_ID")]
        [ForeignKey(nameof(Author))]
        public long AuthorId { get; set; }
        public Member Author
        {
            get => _lazyLoader.Load(this, ref _author);
            set
            {
                _author = value;
                AuthorId = _author.ID;
            }
        }
        #endregion

        #region Constructors
        public Project() { }

        public Project(ILazyLoader lazyLoader) : this()
        {
            _lazyLoader = lazyLoader;
        }

        public Project(long id, string name, Member author) : this()
        {
            ID = id;
            Name = name;
            Author = author;
        }
        #endregion

        #region Methods
        public override string ToString() =>
            $"Project Name: {Name}; Created by: {Author}";
        #endregion
    }
}
