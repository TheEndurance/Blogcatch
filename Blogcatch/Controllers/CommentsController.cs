using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogcatch.Models;
using Blogcatch.ViewModel.Front;

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
        public ActionResult AddComment()
        {
            return View();
        }

        // GET : /Comments/GetChildrenComments/id
        public ActionResult GetChildrenComments(int id)
        {
            var commentVM = _context.Comments
                .Where(x => x.ParentCommentId == id)
                .Include(x=>x.Author)
                .ToArray()
                .Select(x => new CommentViewModel(x))
                .ToList();

            return PartialView("_ChildComments", commentVM);
        }
    }
}