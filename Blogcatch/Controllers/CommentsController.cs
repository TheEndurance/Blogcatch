using Blogcatch.Areas.Admin.Models;
using Blogcatch.Models;
using Blogcatch.Models.Dto;
using Blogcatch.ViewModel.Front;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace Blogcatch.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // POST: /Comments/AddComment
        [HttpPost]
        public ActionResult AddComment(CommentDto commentDto)
        {
            var userId = User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Post", "Blog", new { title = commentDto.PostSlug });
            }

            //Create the new comment
            var newComment = new Comment
            {
                AuthorId = userId,
                Body = commentDto.Body,
                PostedDate = DateTime.Now
            };


            if (commentDto.CommentId == null) //This is a comment for the post
            {
                var post = _context.Posts.Find(commentDto.PostId);
                post.Comments.Add(newComment);
            }
            else //This is a reply to another comment
            {
                //The comment being replied to
                var commentBeingRepliedTo = _context.Comments.Find(commentDto.CommentId);

                if (commentBeingRepliedTo.ParentCommentId == null) // this comment isn't nested
                {
                    //add the comment to the post
                    var post = _context.Posts.Find(commentDto.PostId);
                    newComment.ParentCommentId = commentBeingRepliedTo.Id;
                    post.Comments.Add(newComment);
                }
                else //this comment is nested
                {
                    //find the parent comment, and add the new comment to it
                    var parentComment = _context.Comments.Find(commentBeingRepliedTo.ParentCommentId);

                    newComment.ParentCommentId = parentComment.Id;
                    newComment.PostId = commentDto.PostId;

                    parentComment.Comments.Add(newComment);
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Post", "Blog", new { title = commentDto.PostSlug });
        }

        // GET : /Comments/GetChildrenComments/id
        [HttpGet]
        public ActionResult GetChildrenComments(int id)
        {
            var commentVM = _context.Comments
                .Where(x => x.ParentCommentId == id)
                .Include(x => x.Author)
                .OrderByDescending(x => x.PostedDate)
                .ToArray()
                .Select(x => new CommentViewModel(x))
                .ToList();

            Thread.Sleep(1000);
            return PartialView("_ChildComments", commentVM);
        }
    }
}