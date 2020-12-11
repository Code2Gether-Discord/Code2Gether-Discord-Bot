using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public class Project : IDataModel
    {
        #region Properies
        [Key]
        public virtual int ID { get; set; }
        [Required]
        public virtual string Name { get; set; }
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
