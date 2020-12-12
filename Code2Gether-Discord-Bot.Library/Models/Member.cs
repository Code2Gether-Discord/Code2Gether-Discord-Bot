using System;
using Discord;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Code2Gether_Discord_Bot.Library.Models
{
    [Table("MEMBER")]
    public class Member : IDataModel
    {
        #region Properties
        [Column("MEMBER_ID")]
        [Key]
        public virtual int ID { get; set; }
        [Column("SNOWFLAKE_ID")]
        [Required]
        public virtual ulong SnowflakeId { get; set; }
        public virtual List<Project> Projects { get; set; } = new List<Project>();
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
