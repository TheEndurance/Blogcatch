using Blogcatch.Areas.Admin.Models;
using Blogcatch.Models;
using Blogcatch.ViewModel;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            var categories = _context.Categories.ToList();
            var postVM = new PostViewModel();
            postVM.Categories = categories;
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

            //create new post
            var post = new Post(postVM,userId);

            if (_context.Posts.Any(p => p.Title == postVM.Title || p.Slug == post.Slug))
            {
                ModelState.AddModelError("", "That title or slug already exists");
            }

            //handling post tags
            if (postVM.Tags.Length > 0)
            {
                //Deserialize JSON array to a list of strings
                var tags = JsonConvert.DeserializeObject<List<string>>(postVM.Tags);
                foreach (string tagName in tags)
                {

                    var tag = _context.Tags.SingleOrDefault(x => x.Name == tagName);
                    if (tag == null) //check if the tag doesn't exist
                    {
                        tag = new Tag(tagName);
                        _context.Tags.Add(tag);
                    }
                    post.AttachTag(tag);
                }
            }
  
            _context.Posts.Add(post);
            _context.SaveChanges();

            TempData["SM"] = "Post successfully added!";

            var categories = _context.Categories.ToList();
            postVM.Categories = categories;

            return View("PostForm", postVM);
        }
    }
}