using Blogcatch.Areas.Admin.Models;
using Blogcatch.ViewModel;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Blogcatch.Models;

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
            var categories = _context.Categories.ToList();
            if (!ModelState.IsValid)
            {
                postVM.Categories = categories;
                return View("PostForm", postVM);
            }
            //create new post
            var post = new Post(postVM,userId);

            if (_context.Posts.Any(p => p.Title == postVM.Title || p.Slug == post.Slug))
            {
                ModelState.AddModelError("", "That title or slug already exists");
                postVM.Categories = categories;
                return View("PostForm", postVM);
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
            postVM = new PostViewModel(post);
            postVM.Categories = categories;

            return View("PostForm", postVM);
        }

        //GET : Admin/Posts/EditPost
        public ActionResult EditPost(int id)
        {
            var post = _context.Posts.Include(x=>x.PostTags.Select(p=>p.Tag)).SingleOrDefault(x=>x.Id==id);
            if (post == null)
            {
                return Content("Post not found");
            }
            var postVM = new PostViewModel(post);
            postVM.Categories = _context.Categories.ToList();

            return View("PostForm",postVM);
        }
        // POST : Admin/Posts/EditPost
        [HttpPost]
        public ActionResult EditPost(PostViewModel postVM)
        {
            var categories = _context.Categories.ToList();
            //check if valid model
            if (!ModelState.IsValid)
            {
                postVM.Categories = categories;
                return View("PostForm", postVM);
            }

            //find the post being edited
            var post = _context.Posts.Include(p=>p.PostTags).SingleOrDefault(x => x.Id == postVM.Id);
            //check if it is null before going further
            if (post == null)
            {
                return Content("That post can not be edited, because it no longer exists");
            }
            //set all the new values of the post
            post.UpdatePostValues(postVM);

            //check title/slug
            if (_context.Posts.Where(p=>p.Id!=post.Id).Any(p => p.Title == postVM.Title || p.Slug == post.Slug))
            {
                ModelState.AddModelError("", "That title or slug already exists");
                postVM.Categories = categories;
                return View("PostForm", postVM);
            }

            //Handle Add/Remove of the Post Tags
            var postTagNames = _context.PostTags.Include(x=>x.Tag).Where(p => p.PostId == post.Id).Select(x=>x.Tag.Name).ToList();
            
            if (postVM.Tags.Length > 0) //If there are any tags in the view model
            {
                //Deserialize JSON array to a list of strings
                var tags = JsonConvert.DeserializeObject<List<string>>(postVM.Tags);

                foreach (string postTagName in postTagNames)
                {
                    //Remove the post tag
                    //Check if the current tagName has been removed from the post
                    if (!tags.Contains(postTagName))
                    {
                        var removedPostTag = _context.PostTags.Include(x => x.Tag).Where(p => p.Tag.Name == postTagName).Single();
                        post.RemoveTag(removedPostTag);
                    }
                }
                

                foreach (string tagName in tags)
                {
                    var tag = _context.Tags.SingleOrDefault(x => x.Name == tagName);
                    if (tag == null) //check if the tag doesn't exist
                    {
                        tag = new Tag(tagName);
                        _context.Tags.Add(tag);
                    }
                    //Add the post tag
                    //Check if that Tag is already attached to the post before adding it
                    if (!_context.PostTags.Include(x => x.Tag).Any(x => x.Post.Id == post.Id && x.Tag.Name == tagName))
                    {
                        post.AttachTag(tag);
                    }
                }
            } else //there are no tags on the view model
            {
                //check to see if there are any tags on the post, and delete them if there are
                var postTags = _context.PostTags.Where(p => p.PostId == post.Id).ToList();
                if (postTags.Count > 0)
                {
                    foreach (var item in postTags)
                    {
                        _context.PostTags.Remove(item);
                    }
                }
            }

            _context.SaveChanges();
            TempData["SM"] = "Post successfully edited!";
            postVM = new PostViewModel(post);
            postVM.Categories = _context.Categories.ToList();

            return View("PostForm",postVM);
        }

        // DELETE : /Admin/Posts/DeletePost/id
        [HttpDelete]
        public ActionResult DeletePost(int id)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
            {
                return Json(new {success = false});
            }
            _context.Posts.Remove(post);
            _context.SaveChanges();
            return Json(new {success = true});
        }

        // GET : /Admin/Posts/PostDetails/id
        [HttpGet]
        public ActionResult PostDetails(int id)
        {
            var post = _context.Posts.Include(x=>x.Category).Include(x=>x.PostTags.Select(p=>p.Tag)).SingleOrDefault(x=>x.Id==id);
            if (post == null)
            {
                return Content("That post no longer exists");
            }
            var postVM = new PostViewModel(post);
            return View(postVM);
        }
    }

    
}