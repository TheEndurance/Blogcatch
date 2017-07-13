using Blogcatch.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations;

namespace Blogcatch.ViewModel
{
    public class PageViewModel
    {
        public PageViewModel(Page page)
        {
            this.Title = page.Title;
            this.Slug = page.Slug;
            this.Body = page.Body;
            this.Sorting = page.Sorting;
            this.HasSidebar = page.HasSidebar;
        }
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Slug { get; set; }

        [MinLength(3)]
        public string Body { get; set; }
        public int Sorting { get; set; }

        public bool HasSidebar { get; set; }
    }
}