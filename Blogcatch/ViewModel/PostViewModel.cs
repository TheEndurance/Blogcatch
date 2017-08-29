using Blogcatch.Areas.Admin.Controllers;
using Blogcatch.Areas.Admin.Models;
using Blogcatch.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

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

        [Required]
        [AllowHtml]
        public string Content { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Slug { get; set; }

        public string DisplayPicture { get; set; }

        public HttpPostedFileBase Image { get; set; }

        public string Excerpt { get; set; }

        [Display(Name = "Comments Allowed")]
        public bool AllowComments { get; set; }

        [Required]
        [Display(Name = "Post Category")]
        public int CategoryId { get; set; }

        public string Tags { get; set; }

        public IEnumerable<Category> Categories { get; set; }



        public string Heading
        {
            get { return ((Id == 0) ? "Add a new post" : "Edit post"); }
        }

        public string Action
        {
            get
            {
                Expression<Func<PostsController, ActionResult>> AddPost = x => x.AddPost();
                Expression<Func<PostsController, ActionResult>> EditPost = x => x.EditPost(null);
                var action = (Id == 0) ? AddPost : EditPost;
                return (action.Body as MethodCallExpression).Method.Name;

            }
        }


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
            this.DisplayPicture = post.DisplayPicture;
            this.Title = post.Title;
            this.Tags = post.GetJsonPostTags();
            this.CategoryId = post.CategoryId;
            this.Slug = post.Slug;
            this.Excerpt = post.Excerpt;
            this.AllowComments = post.AllowComments;
        }
    }
}