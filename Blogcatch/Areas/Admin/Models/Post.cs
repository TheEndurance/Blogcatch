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

        public string DisplayPicture { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Slug { get; set; }

        public string Excerpt { get; set; }

        public bool AllowComments { get; set; }

        public ICollection<PostTag> PostTags { get; set; }

        public Category Category { get; set; }


        public Post()
        {
            PostTags = new Collection<PostTag>();
        }


        public static string TruncateHtml(string input, int length = 300,
            string ommission = "...")
        {
            if (input == null || input.Length < length)
                return input;
            int lastSpace = input.LastIndexOf(" ", length);
            var excerpt = string.Format("{0}" + ommission, input.Substring(0, (lastSpace > 0) ?
                lastSpace : length));
            return excerpt.Trim();
        }
    }
}