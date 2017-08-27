using System.ComponentModel.DataAnnotations;

namespace Blogcatch.Areas.Admin.Models
{
    public class Tag
    {
        public Tag(string name)
        {
            this.Name = name;
        }

        public Tag()
        {

        }
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}