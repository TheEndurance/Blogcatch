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

            var activeWidgets = _context.Widgets.Where(x=>x.Enabled).Select(x=>x.Type).ToList();

            var blogVM = new BlogViewModel
            {
                BlogPostViewModels = blogPostVM,
                ActiveWidgets = activeWidgets
            };
            
            return View(blogVM);
        }


        public ActionResult Search(string query)
        {
            throw new System.NotImplementedException();
        }

        public ActionResult SearchByCategory(string query)
        {
            throw new System.NotImplementedException();
        }
    }
}
