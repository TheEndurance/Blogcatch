﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Blogcatch.Models;

namespace Blogcatch.Areas.Admin.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public ApplicationUser Author { get; set; }

        [Required]
        [ForeignKey(nameof(Author))]
        public string AuthorId { get; set; }

        public DateTime PostedDate { get; set; }

        [StringLength(500)]
        [Required]
        public string Body { get; set; }

        public Post Post { get; set; }

        [Required]
        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }

        public Comment ParentComment { get; set; }

        public int? ParentCommentId { get; set; }

        //fill this collection by doing looking for comments whoses ParentCommentId is the same as this comments Id
        public ICollection<Comment> Comments { get; set; } = new Collection<Comment>();

        public Comment()
        {
            
        }
    }
}