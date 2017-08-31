using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blogcatch.Areas.Admin.Models
{
    public class Widget
    {
        public int Id { get; set; }

        [Required]
        public short Sorting { get; set; }

        [Required]
        public bool Enabled { get; set; }

        [Required]
        [StringLength(255)]
        public string Type { get; set; }
    }
}