using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;

namespace Code2Gether_Discord_Bot.Library.Models
{
    [Table("PROJECT")]
    public class Project : IDataModel
    {
        #region Properies
        [Column("PROJECT_ID")]
        [Key]
        public virtual int ID { get; set; }
        [Column("PROJECT_NAME")]
        [Required]
        public virtual string Name { get; set; }
        [Column("AUTHOR_ID")]
        public virtual Member Author { get; set; }
        public virtual List<Member> Members { get; set; } = new List<Member>();
        [NotMapped]
        public bool IsActive => Members.Count() >= 2;
        #endregion

        #region Constructors
        public Project() { }

        public Project(int id, string name, Member author) : this()
        {
            ID = id;
            Name = name;
            Author = author;
        }
        #endregion
    }
}
