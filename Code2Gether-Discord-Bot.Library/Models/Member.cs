using System;
using Discord;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public class Member : IDataModel
    {
        #region Properties
        [Column("MEMBER_ID")]
        [Key]
        public int ID { get; set; }
        [Column("MEMBER_SNOWFLAKE")]
        public ulong SnowflakeId { get; set; }
        [JsonIgnore]
        [NotMapped]
        public IUser DiscordUserInfo { get; set; }
        public List<Project> Projects { get; set; } = new List<Project>();
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
