using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Code2Gether_Discord_Bot.Library.Models
{
    [Table("PROJECT_MEMBER")]
    public class ProjectMember
    {
        #region Fields
        private Project project;
        private Member member;
        #endregion

        #region Properties
        [JsonIgnore]
        [Column("PROJECT_ID")]
        public int ProjectID { get; set; }
        [JsonIgnore]
        [Column("MEMBER_ID")]
        public int MemberID { get; set; }
        #endregion
    }
}
