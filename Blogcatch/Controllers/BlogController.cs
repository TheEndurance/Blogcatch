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
        public ActionResult Index(int? page,string search,string category)
        {
            //get the blogposts
            var blogPostVM = _context.Posts
                .Include(x => x.Author)
                .Include(x=>x.Category)
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
            var pager = new Pager(blogPostVM.Count(),page,2);

            //get the active widget names
            var activeWidgets = _context.Widgets.Where(x=>x.Enabled).Select(x=>x.Type).ToList();

            //create the main viewmodel
            var blogVM = new BlogViewModel
            {
                BlogPostViewModels = blogPostVM.Skip((pager.CurrentPage-1)*pager.NumItemsPerPage).Take(pager.NumItemsPerPage).ToList().Select(x=>new BlogPostViewModel(x)),
                ActiveWidgets = activeWidgets,
                Pager = pager,
                Search = search,
                Category = category
            };
            
            return View(blogVM);
        }


        public ActionResult SearchByCategory(string query)
        {
            throw new System.NotImplementedException();
        }
    }
}
