using Blogcatch.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

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
        public ActionResult Index()
        {
            var blogPostVM = _context.Posts
                .Include(x => x.Author)
                .Include(x => x.PostTags.Select(p => p.Tag))
                .OrderByDescending(x => x.PostDate)
                .Take(5)
                .ToArray()
                .Select(x => new BlogPostViewModel(x))
                .ToList();
            return View(blogPostVM);
        }
    }
}