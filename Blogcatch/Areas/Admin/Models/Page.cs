using System.ComponentModel.DataAnnotations;

namespace Blogcatch.Areas.Admin.Models
{
    public class Page
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Slug { get; set; }


        public string Body { get; set; }

        public int Sorting { get; set; }

        public bool HasSidebar { get; set; }
    }
}