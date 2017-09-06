using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Blogcatch.Areas.Admin.Models;
using Blogcatch.Models;

namespace Blogcatch.ViewModel.Front
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public ApplicationUser Author { get; set; }

        public DateTime PostedDate { get; set; }
        
        [Required]
        public string Body { get; set; }

        public int PostId { get; set; }


        //fill this collection by doing looking for comments whoses ParentCommentId is the same as this comments Id

        public int ChildrenCommentCount { get; set; }

        public CommentViewModel(Comment comment)
        {
            this.Id = comment.Id;
            this.Author = comment.Author;
            this.PostedDate = comment.PostedDate;
            this.Body = comment.Body;
            this.PostId = comment.PostId;
        }
    }
}