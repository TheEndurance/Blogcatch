using System.Linq;
using System.Web.Mvc;
using Blogcatch.Models;
using Blogcatch.ViewModel;

namespace Blogcatch.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            var pages = _context.Pages
                .ToArray()
                .OrderBy(p => p.Sorting)
                .Select(p => new PageViewModel(p))
                .ToList();
  
            return View(pages);
        }
    }
}