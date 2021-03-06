﻿using Blogcatch.Areas.Admin.Models;
using Blogcatch.Models;
using System;
using System.Collections.Generic;

namespace Blogcatch.ViewModel.Front
{
    public class BlogPostDetailViewModel
    {
        public int Id { get; set; }
        public ApplicationUser Author { get; set; }

        public DateTime PostDate { get; set; }

        public string Content { get; set; }

        public string DisplayPicture { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }

        public bool AllowComments { get; set; }

        public List<CommentViewModel> CommentViewModels { get; set; }

        public BlogPostDetailViewModel(Post post)
        {
            this.Id = post.Id;
            this.Author = post.Author;
            this.PostDate = post.PostDate;
            this.Content = post.Content;
            this.Title = post.Title;
            this.Slug = post.Slug;
            this.AllowComments = post.AllowComments;
        }

    }
}