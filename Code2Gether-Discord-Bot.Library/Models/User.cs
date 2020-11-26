using System;
using Discord;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public class User
    {
        #region Properties
        [Key]
        public long ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [NotMapped]
        public IUser DiscordUserInfo { get; set; }
        #endregion

        #region Constructor
        public User() 
        {

        }

        public User(IUser user) : this()
        {
            DiscordUserInfo = user;
            UserName = DiscordUserInfo.Username;
        }
        #endregion
    }
}
