using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogcatch.Models.Dto
{
    public class CommentDto
    {
        public int Id { get; set; }

        public ApplicationUser Author { get; set; }

        public string AuthorId { get; set; }

        public DateTime PostedDate { get; set; }

        public string Body { get; set; }

        public int PostId { get; set; }

        public int? ParentCommentId { get; set; }
    }
}