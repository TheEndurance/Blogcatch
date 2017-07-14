using Blogcatch.ViewModel;
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


        public static Page CreatePage(PageViewModel pageVM)
        {
            var page = new Page();

            page.Title = pageVM.Title;

            var slug = (string.IsNullOrWhiteSpace(pageVM.Slug))
                ? pageVM.Title.Replace(" ", "-").ToLower()
                : pageVM.Slug.Replace(" ", "-").ToLower();

            page.Slug = slug;
            page.Body = pageVM.Body;
            page.HasSidebar = pageVM.HasSidebar;
            page.Sorting = 100;
            return page;
        }
    }
}