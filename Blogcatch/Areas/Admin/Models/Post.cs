using Blogcatch.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;
using Blogcatch.Models;
using Blogcatch.ViewModel.Admin;

namespace Blogcatch.Areas.Admin.Models
{
    public class Post
    {
        public int Id { get; set; }

        public ApplicationUser Author { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [Required]
        public DateTime PostDate { get; set; }

        public DateTime? PostModifiedDate { get; set; }

        public string Content { get; set; }

        public string DisplayPicture { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Slug { get; set; }

        public string Excerpt { get; set; }

        public bool AllowComments { get; set; }

        public ICollection<PostTag> PostTags { get; set; }


        public Category Category { get; set; }

        [ForeignKey("Category")]
        [Required]
        public int CategoryId { get; set; }



        public Post(PostViewModel postVM, string userId)
        {
            if (postVM == null)
            {
                throw new ArgumentNullException(nameof(postVM));
            }
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            PostTags = new Collection<PostTag>();
            this.AuthorId = userId;
            this.PostDate = DateTime.Now;
            this.AllowComments = postVM.AllowComments;
            this.Content = postVM.Content;
            this.CategoryId = postVM.CategoryId;
            SetExcerpt(postVM.Content);
            SetSlug(postVM.Title, postVM.Slug);
            this.DisplayPicture = ImageManager.GetImageFilePath(postVM.Image);
        }

        public Post()
        {
            PostTags = new Collection<PostTag>();
        }


        public void UpdatePostValues(PostViewModel postVM)
        {
            this.PostModifiedDate = DateTime.Now;
            this.AllowComments = postVM.AllowComments;
            this.Content = postVM.Content;
            this.CategoryId = postVM.CategoryId;
            SetExcerpt(postVM.Content);
            SetSlug(postVM.Title, postVM.Slug);
            this.DisplayPicture = ImageManager.GetImageFilePath(postVM.Image);
        }
        /// <summary>
        /// Creates the blog post excerpt by truncating the content length
        /// </summary>
        private void SetExcerpt(string input, int length = 500,
            string ommission = "...")
        {
            if (input == null || input.Length < length)
            {
                this.Excerpt = input;
                return;
            }
            int lastSpace = input.LastIndexOf(" ", length);
            var excerpt = $"{input.Substring(0, (lastSpace > 0) ? lastSpace : length)}" + ommission;
            this.Excerpt = excerpt.Trim();
        }

        /// <summary>
        /// Set the post slug
        /// </summary>
        private void SetSlug(string postTitle, string postSlug)
        {
            string slug = "";

            this.Title = postTitle;
            slug = (string.IsNullOrWhiteSpace(postSlug))
                ? postTitle.Replace(" ", "-")
                : postSlug.Replace(" ", "-");
            this.Slug = slug;

        }

        /// <summary>
        /// Set the display picture image url
        /// </summary>
        private void SetDisplayPicture(HttpPostedFileBase Image)
        {
            if (Image != null && Image.ContentLength > 0)
            {
                var fileName = Path.GetFileName(Image.FileName);
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/fileman/Uploads/Images"), fileName);
                Image.SaveAs(path);
                this.DisplayPicture = ("/fileman/Uploads/Images/" + fileName);
            }
        }

        /// <summary>
        /// Links a tag and post to create the intermediary PostTag table entry
        /// </summary>
        public void AttachTag(Tag tag)
        {
            var postTag = new PostTag(this, tag);
            PostTags.Add(postTag);
        }

        public void RemoveTag(PostTag postTag)
        {
            PostTags.Remove(postTag);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetJsonPostTags()
        {
            var tags = PostTags.Select(x => x.Tag.Name).ToList();
            var jsonTags = JsonConvert.SerializeObject(tags);
            return jsonTags;
        }

    }
}