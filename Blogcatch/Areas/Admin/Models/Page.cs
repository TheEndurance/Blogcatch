using Blogcatch.ViewModel;
using System.ComponentModel.DataAnnotations;
using Blogcatch.ViewModel.Admin;

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

        public string HeaderPicture { get; set; }


        public Page(PageViewModel pageVM)
        {
            this.Title = pageVM.Title;
            this.Slug = CreateSlug(pageVM.Slug, pageVM.Title);
            this.Body = pageVM.Body;
            this.HasSidebar = pageVM.HasSidebar;
            this.Sorting = 100;
            this.HeaderPicture = ImageManager.GetImageFilePath(pageVM.Image);
        }

        protected Page()
        {
        }


        public void EditPage(PageViewModel pageVM)
        {
            this.Slug = CreateSlug(pageVM.Slug, pageVM.Title);
            this.Title = pageVM.Title;
            this.Body = pageVM.Body;
            this.HasSidebar = pageVM.HasSidebar;
            this.HeaderPicture = ImageManager.GetImageFilePath(pageVM.Image);
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