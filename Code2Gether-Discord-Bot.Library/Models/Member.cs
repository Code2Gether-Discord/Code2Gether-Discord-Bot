using System;
using Discord;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Code2Gether_Discord_Bot.Library.Models
{
    [Table("MEMBER")]
    public class Member
    {
        #region Properties
        [Column("MEMBER_ID")]
        [Key]
        public long ID { get; set; }
        [Column("MEMBER_SNOWFLAKE")]
        public ulong SnowflakeId { get; set; }
        [JsonIgnore]
        [NotMapped]
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
    }
}
