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

            //set post values and create the post

            post.Slug = slug;
            post.PostDate = DateTime.Now;
            post.AllowComments = postVM.AllowComments;
            post.Content = postVM.Content;
            post.Excerpt = excerpt;
            post.AuthorId = userId;
            post.CategoryId = postVM.CategoryId;
            _context.Posts.Add(post);
            _context.SaveChanges();

            //handling post tags
            if (postVM.Tags.Length > 0)
            {
                //Deserialize JSON array to a list of strings
                var tags = JsonConvert.DeserializeObject<List<string>>(postVM.Tags);
                foreach (string tagName in tags)
                {

                    var tag = _context.Tags.SingleOrDefault(x => x.Name == tagName);
                    if (tag != null) //check if the tag already exists
                    {
                        var postTag = new PostTag(post.Id, tag.Id);
                        _context.PostTags.Add(postTag);
                    }
                    else // otherwise create the tag
                    {
                        tag = new Tag(tagName);
                        _context.Tags.Add(tag);
                        _context.SaveChanges();

                        var postTag = new PostTag(post.Id, tag.Id);
                        _context.PostTags.Add(postTag);
                        _context.SaveChanges();

                    }

                }
            }


            TempData["SM"] = "Post successfully added!";

            var categories = _context.Categories.ToList();
            postVM.Categories = categories;

            return View("PostForm", postVM);
        }
    }
}