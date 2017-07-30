using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogcatch.Areas.Admin.Models
{
    public class PostTag
    {
        public Post Post { get; set; }
        public Tag Tag { get; set; }

        [Key]
        [Column(Order = 1)]
        public int PostId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int TagId { get; set; }
    }
}