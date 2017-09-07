using Blogcatch.Models;
using Blogcatch.Models.Dto;
using Blogcatch.ViewModel.Front;
using Microsoft.AspNet.Identity;
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



            return View();
        }

        // GET : /Comments/GetChildrenComments/id
        [HttpGet]
        public ActionResult GetChildrenComments(int id)
        {
            var commentVM = _context.Comments
                .Where(x => x.ParentCommentId == id)
                .Include(x => x.Author)
                .ToArray()
                .Select(x => new CommentViewModel(x))
                .ToList();

            Thread.Sleep(1000);
            return PartialView("_ChildComments", commentVM);
        }
    }
}