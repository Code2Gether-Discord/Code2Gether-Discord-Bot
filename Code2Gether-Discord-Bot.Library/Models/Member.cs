using Discord;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Code2Gether_Discord_Bot.Library.Models
{
    [Table("MEMBERS")]
    public class Member : IDataModel
    {
        #region Properties
        [Column("MEMBER_ID")]
        [Key]
        public virtual int ID { get; set; }
        [Column("MEMBER_SNOWFLAKE_ID")]
        public virtual ulong SnowflakeId { get; set; }
        [NotMapped]
        public virtual List<Project> Projects { get; set; } = new List<Project>();
        [NotMapped]
        [JsonIgnore]
        public IUser DiscordUserInfo { get; set; }
        #endregion

        #region Constructor
        public Member() { }

        public Member(IUser user) : this()
        {
            DiscordUserInfo = user;
            SnowflakeId = DiscordUserInfo.Id;
        }
        #endregion

        #region Methods
        public override string ToString() => $"{SnowflakeId}";
        #endregion
    }
}
