using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Blogcatch.Models;
using Blogcatch.ViewModel;
using Blogcatch.ViewModel.Front;

namespace Blogcatch.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;


        public BlogController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Blog
        public ActionResult Index(int? page, string search, string category)
        {
            //get the blogposts
            var blogPostVM = _context.Posts
                .Include(x => x.Author)
                .Include(x => x.Category)
                .Include(x => x.PostTags.Select(p => p.Tag));
            //check if user is searching
            if (!string.IsNullOrWhiteSpace(search))
                blogPostVM = blogPostVM
                    .Where(x => x.Content.Contains(search) || x.Title.Contains(search));
            //check if user is searching by category
            if (!string.IsNullOrWhiteSpace(category))
                blogPostVM = blogPostVM.Where(x => x.Category.Name == category);

            //order descending time
            blogPostVM = blogPostVM.OrderByDescending(x => x.PostDate);

            //create pager
            var pager = new Pager(blogPostVM.Count(), page, 2);

            //get the active widget names
            var activeWidgets = _context.Widgets.Where(x => x.Enabled).Select(x => x.Type).ToList();

            //create the main viewmodel
            var blogVM = new BlogViewModel
            {
                BlogPostViewModels = blogPostVM
                    .Skip((pager.CurrentPage - 1) * pager.NumItemsPerPage)
                    .Take(pager.NumItemsPerPage).ToList()
                    .Select(x => new BlogPostViewModel(x)),
                ActiveWidgets = activeWidgets,
                Pager = pager,
                Search = search,
                Category = category
            };

            return View(blogVM);
        }

        // GET : /Blog/Post?title=
        public ActionResult Post(string title)
        {
            var post = _context.Posts
                .Include(x => x.Author)
                .SingleOrDefault(x => x.Slug == title);

            if (post == null)
                return Content("This post does not exist or has been removed");

            var blogPostDetailVM = new BlogPostDetailViewModel(post);

            if (post.AllowComments)
            {
                var commentVM = _context.Comments
                    .Where(x => x.PostId == post.Id)
                    .ToArray()
                    .Select(x => new CommentViewModel(x))
                    .ToList();

                //get the counts of children comments into a dictionary with the key as a Parent comment Id, and the value as the count of children comments for that parent comment
                var _counts = _context.Comments.GroupBy(x => x.ParentCommentId)
                    .ToDictionary(d => d.Key, d => d.Count());

                //assigning the counts of children comments to each comment
                foreach (var c in commentVM)
                {
                    c.ChildrenCommentCount = _counts.ContainsKey(c.Id) ? _counts[c.Id] : 0;
                }

                blogPostDetailVM.CommentViewModels = commentVM;
            }
            
            return View(blogPostDetailVM);
        }
        
        //GET : /blog/Page?title=
        public ActionResult Page(string title)
        {
            var page = _context.Pages.SingleOrDefault(x => x.Slug == title);
            if (page == null)
                return Content("This page doesn't exist or has been removed.");

            var pageVM = new PageViewModel(page);
            pageVM.Sidebar = _context.Sidebar.Find(1);
            return View(pageVM);
        }
    }
}
