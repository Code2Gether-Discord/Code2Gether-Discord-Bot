using System;
using Discord;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public class User
    {
        #region Properties
        [Key]
        public long ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [JsonIgnore]
        [NotMapped]
        public IUser DiscordUserInfo { get; set; }
        [NotMapped]
        public ProjectRole role { get; set; }
        #endregion

        #region Constructor
        public User() { }

        public User(IUser user) : this()
        {
            DiscordUserInfo = user;
            UserName = DiscordUserInfo.Username;
        }
        #endregion
    }
}
