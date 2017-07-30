using Blogcatch.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

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

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Slug { get; set; }

        public string Excerpt { get; set; }

        public bool AllowComments { get; set; }

        public ICollection<PostTag> PostTags { get; set; }

        public ICollection<PostCategory> PostCategories { get; set; }


        public Post()
        {
            PostTags = new Collection<PostTag>();

            PostCategories = new Collection<PostCategory>();
        }

    }
}