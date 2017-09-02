using System.Linq;
using System.Web.Mvc;
using Blogcatch.Models;
using Blogcatch.ViewModel;
using Blogcatch.ViewModel.Admin;

namespace Blogcatch.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController()
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