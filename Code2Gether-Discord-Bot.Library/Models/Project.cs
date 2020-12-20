using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public Project(string name, Member author) : this()
        {
            Name = name;
            Author = author;
        }
        #endregion

        #region Methods

        public override string ToString()
        {
            var nl = Environment.NewLine;

            var sb = new StringBuilder();

            sb.Append($"Project Name: {Name}{nl}");
            sb.Append($"Author: {Author}{nl}");

            sb.Append($"Project Members:{nl}");
            foreach (var member in Members)
            {
                sb.Append($"\t{member}{nl}");
            }

            return sb.ToString();
        }

        #endregion
    }
}
