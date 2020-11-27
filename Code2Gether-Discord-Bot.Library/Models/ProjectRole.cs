using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public class ProjectRole
    {
        [Key]
        public long ID { get; set; }
        [Required]
        public string RoleName { get; set; }
        // todo: role privileges
        public bool CanReadData { get; set; }
        public bool CanWriteData { get; set; }
        public bool IsAdmin { get; set; }
    }
}
