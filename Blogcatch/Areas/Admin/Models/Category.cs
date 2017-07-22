using System.ComponentModel.DataAnnotations;

namespace Blogcatch.Areas.Admin.Models
{
    public class Category
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Slug { get; set; }
        public int? Sorting { get; set; }

        public Category()
        {
        }

        public Category(string categoryName)
        {
            var trimmed = categoryName.Trim();
            this.Name = trimmed;
            this.Slug = trimmed.Replace(" ", "-");
            this.Sorting = 100;
        }
    }
}