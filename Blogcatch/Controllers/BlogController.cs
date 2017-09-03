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
        public ActionResult Index(int? page)
        {
            //get the blogposts
            var blogPostVM = _context.Posts
                .Include(x => x.Author)
                .Include(x => x.PostTags.Select(p => p.Tag))
                .OrderByDescending(x => x.PostDate);

            //create pager
            var pager = new Pager(blogPostVM.Count(),page);

            //get the active widget names
            var activeWidgets = _context.Widgets.Where(x=>x.Enabled).Select(x=>x.Type).ToList();

            //create the main viewmodel
            var blogVM = new BlogViewModel
            {
                BlogPostViewModels = blogPostVM.Skip((pager.CurrentPage-1)*pager.NumItemsPerPage).Take(pager.NumItemsPerPage).ToList().Select(x=>new BlogPostViewModel(x)),
                ActiveWidgets = activeWidgets,
                Pager = pager
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
