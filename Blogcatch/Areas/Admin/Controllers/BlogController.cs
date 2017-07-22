using Blogcatch.Models;
using System.Linq;
using System.Web.Mvc;
using Blogcatch.ViewModel;

namespace Blogcatch.Areas.Admin.Controllers
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
        // GET: Admin/Blog/Categories
        public ActionResult Categories()
        {
            var categoryVMList = _context.Categories.ToArray().Select(x=>new CategoryViewModel(x)).OrderBy(x => x.Sorting).ToList();
            return View(categoryVMList);
        }
    }
}