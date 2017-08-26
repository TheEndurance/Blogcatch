using Blogcatch.Areas.Admin.Models;
using Blogcatch.Models;
using Blogcatch.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Blogcatch.Areas.Admin.Controllers
{



    public class PostsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public PostsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Admin/Posts
        public ActionResult Index()
        {
            var postVMList = _context.Posts.ToArray().Select(x => new PostViewModel(x)).OrderBy(p => p.PostDate)
                .ToList();
            return View(postVMList);
        }

        // GET : Admin/Posts/AddPost
        [HttpGet]
        public ActionResult AddPost()
        {
            var postVM = new PostViewModel();
            return View("PostForm", postVM);
        }

        // POST : Admin/Posts/AddPost
        [HttpPost]
        public ActionResult AddPost(PostViewModel postVM)
        {
            var userId = User.Identity.GetUserId();

            if (!ModelState.IsValid)
            {
                return View("PostForm", postVM);
            }
            var post = new Post();

            string slug = "";

            post.Title = postVM.Title;
            slug = (string.IsNullOrWhiteSpace(postVM.Slug))
                ? postVM.Title.Replace(" ", "-")
                : postVM.Slug.Replace(" ", "-");

            if (_context.Posts.Any(p => p.Title == postVM.Title || p.Slug == slug))
            {
                ModelState.AddModelError("", "That title or slug already exists");
            }

            string excerpt = Post.TruncateHtml(postVM.Content);

            int length = 300;
            if (postVM.Content.Length > length)
            {
                int lastSpace = postVM.Content.LastIndexOf(" ", length);
                excerpt = $"{postVM.Content.Substring(0, (lastSpace > 0) ? lastSpace : length)}" + "...";

            }

            //handling image
            if (postVM.Image != null && postVM.Image.ContentLength > 0)
            {
                var fileName = Path.GetFileName(postVM.Image.FileName);
                var path = Path.Combine(Server.MapPath("~/fileman/Uploads/Images"), fileName);
                postVM.Image.SaveAs(path);
                postVM.DisplayPicture = ("/fileman/Uploads/Images/" + fileName);
                post.DisplayPicture = postVM.DisplayPicture;
            }


            post.Slug = slug;
            post.PostDate = DateTime.Now;
            post.AllowComments = postVM.AllowComments;
            post.Content = postVM.Content;
            post.Excerpt = excerpt;
            post.AuthorId = userId;
            _context.Posts.Add(post);
            _context.SaveChanges();



            TempData["SM"] = "Post successfully added!";
            return View("PostForm", postVM);
        }
    }
}