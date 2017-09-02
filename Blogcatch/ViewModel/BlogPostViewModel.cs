using System;
using System.Collections.Generic;
using Blogcatch.Areas.Admin.Models;
using Blogcatch.Models;

namespace Blogcatch.ViewModel
{
    public class BlogPostViewModel
    {
        public BlogPostViewModel(Post post)
        {
            this.Id = post.Id;
            this.Author = post.Author;
            this.PostDate = post.PostDate;
            this.DisplayPicture = post.DisplayPicture;
            this.Title = post.Title;
            this.Slug = post.Slug;
            this.Excerpt = post.Excerpt;
            this.AllowComments = post.AllowComments;
            this.PostTags = post.PostTags;
            this.Category = post.Category;
        }
        public int Id { get; set; }

        public ApplicationUser Author { get; set; }

        public DateTime PostDate { get; set; }

        public string DisplayPicture { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }

        public string Excerpt { get; set; }

        public bool AllowComments { get; set; }

        public ICollection<PostTag> PostTags { get; set; }

        public Category Category { get; set; }

        public List<string> ActiveWidgets { get; set; }

    }
}