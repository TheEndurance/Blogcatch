using System;
using System.ComponentModel.DataAnnotations;

namespace Blogcatch.Models.Dto
{
    public class CommentDto
    {
        [Required]
        public int CommentId { get; set; }

        public ApplicationUser Author { get; set; }

        public string AuthorId { get; set; }

        public DateTime PostedDate { get; set; }

        [Required]
        public string Body { get; set; }


        public string PostSlug { get; set; }
    }
}