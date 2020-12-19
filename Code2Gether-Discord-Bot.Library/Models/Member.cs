using Discord;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public class Member : IDataModel
    {
        #region Properties
        [Key]
        public virtual int ID { get; set; }
        public virtual ulong SnowflakeId { get; set; }
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
    }
}
