using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blogcatch.Areas.Admin.Models
{
    public class PostCategory
    {
        public Category Category { get; set; }
        public Post Post { get; set; }

        [Key]
        [Column(Order =1)]
        public int CategoryId { get; set; }

        [Key]
        [Column(Order =2)]
        public int PostId { get; set; }
    }
}