using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public class ProjectMember
    {
        #region Properties
        [Column("PROJECT_ID")]
        [ForeignKey(nameof(Project))]
        public int ProjectID { get; set; }
        [Column("MEMBER_ID")]
        [ForeignKey(nameof(Member))]
        public int MemberID { get; set; }
        [NotMapped]
        public Project Project { get; set; }
        [NotMapped]
        public Member Member { get; set; }
        #endregion
    }
}
