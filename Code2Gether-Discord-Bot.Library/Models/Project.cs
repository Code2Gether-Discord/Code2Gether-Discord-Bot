using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code2Gether_Discord_Bot.Library.Models
{
    [Table("PROJECT")]
    public class Project
    {
        #region Fields
        private readonly ILazyLoader _lazyLoader;
        private Member _author;
        #endregion

        #region Properies
        [Column("PROJECT_ID")]
        [Key]
        public int ID { get; set; }
        [Column("PROJECT_NAME")]
        public string Name { get; set; }
        [Column("MEMBER_ID")]
        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }
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

        public Project(int id, string name, Member author) : this()
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
