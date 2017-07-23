using Blogcatch.Areas.Admin.Models;
using Blogcatch.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Blogcatch.ViewModel
{
    public class PostViewModel
    {

        public int Id { get; set; }

        public ApplicationUser Author { get; set; }

        [Display(Name = "Date Posted")]
        public DateTime PostDate { get; set; }

        [Display(Name = "Date Modified")]
        public DateTime? PostModifiedDate { get; set; }

        public string Content { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Slug { get; set; }

        public string Excerpt { get; set; }

        [Display(Name = "Comments Allowed")]
        public bool AllowComments { get; set; }

        public PostViewModel()
        {
        }

        public PostViewModel(Post post)
        {
            this.Id = post.Id;
            this.Author = post.Author;
            this.PostDate = post.PostDate;
            this.PostModifiedDate = post.PostModifiedDate;
            this.Content = post.Content;
            this.Title = post.Title;
            this.Slug = post.Slug;
            this.Excerpt = post.Excerpt;
            this.AllowComments = post.AllowComments;
        }
    }
}