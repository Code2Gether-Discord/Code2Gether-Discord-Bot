using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace Code2Gether_Discord_Bot.Library.Models
{
    [Table("PROJECTS")]
    public class Project : IDataModel
    {
        #region Fields
        private Member author;
        #endregion

        #region Properies
        [Column("PROJECT_ID")]
        [Key]
        public virtual int ID { get; set; }
        [Column("PROJECT_NAME")]
        [Required]
        public virtual string Name { get; set; }
        [JsonIgnore]
        [Required]
        [Column("AUTHOR_ID")]
        public int AuthorId { get; set; }
        [NotMapped]
        public virtual Member Author 
        {
            get => author;
            set
            {
                author = value;
                AuthorId = author?.ID ?? 0;
            }
        }
        [NotMapped]
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
                sb.Append($"{member}; ");
            }

            return sb.ToString();
        }
        #endregion
    }
}
