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

        public Page(PageViewModel pageVM)
        {
            this.Title = pageVM.Title;
            this.Slug = CreateSlug(pageVM.Slug, pageVM.Title);
            this.Body = pageVM.Body;
            this.HasSidebar = pageVM.HasSidebar;
            this.Sorting = 100;
        }

        protected Page()
        {
        }



        public static void EditPage(PageViewModel pageVM, Page page)
        {

            var slug = (string.IsNullOrWhiteSpace(pageVM.Slug))
                 ? pageVM.Title.Replace(" ", "-").ToLower()
                 : pageVM.Slug.Replace(" ", "-").ToLower();

            page.Slug = slug;
            page.Title = pageVM.Title;
            page.Body = pageVM.Body;
            page.HasSidebar = pageVM.HasSidebar;
        }


        private static string CreateSlug(string inputSlug, string title)
        {
            var slug = (string.IsNullOrWhiteSpace(inputSlug))
                ? title.Replace(" ", "-").ToLower()
                : inputSlug.Replace(" ", "-").ToLower();

            return slug;
        }
    }
}