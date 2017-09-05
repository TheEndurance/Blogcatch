using System;
using System.Collections.Generic;
using Blogcatch.Areas.Admin.Models;
using Blogcatch.Models;

namespace Blogcatch.ViewModel.Front
{
    public class BlogPostDetailViewModel
    {

        public ApplicationUser Author { get; set; }

        public DateTime PostDate { get; set; }

        public string Content { get; set; }

        public string DisplayPicture { get; set; }
        
        public string Title { get; set; }

        public bool AllowComments { get; set; }

        public List<CommentViewModel> CommentViewModels { get; set; }

        public BlogPostDetailViewModel(Post post)
        {
            this.Author = post.Author;
            this.PostDate = post.PostDate;
            this.Content = post.Content;
            this.Title = post.Title;
            this.AllowComments = post.AllowComments;
        }

    }
}