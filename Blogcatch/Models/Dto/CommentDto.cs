using System.ComponentModel.DataAnnotations;

namespace Blogcatch.Models.Dto
{
    public class CommentDto
    {
        public int? CommentId { get; set; }

        public string AuthorId { get; set; }

        [Required]
        public string Body { get; set; }

        public string PostSlug { get; set; }

        [Required]
        public int PostId { get; set; }
    }
}