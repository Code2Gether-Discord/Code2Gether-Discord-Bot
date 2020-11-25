using System;
using System.ComponentModel.DataAnnotations;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public class FakeModel
    {
        [Key]
        public long ID { get; set; }
        [Required]
        public string modelName { get; set; }
        public int modelNumber { get; set; }
        public DateTime? date { get; set; }

        public FakeModel()
        {
            date = DateTime.Now;
        }
    }
}
