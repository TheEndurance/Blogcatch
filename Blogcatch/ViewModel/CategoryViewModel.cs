using Blogcatch.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations;

namespace Blogcatch.ViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Slug { get; set; }
        public int? Sorting { get; set; }

        public CategoryViewModel()
        {
        }

        public CategoryViewModel(Category category)
        {
            this.Id = category.Id;
            this.Name = category.Name;
            this.Slug = category.Slug;
            this.Sorting = category.Sorting;
        }


    }
}