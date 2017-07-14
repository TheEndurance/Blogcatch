using Blogcatch.Models;
using Blogcatch.ViewModel;
using System.Linq;
using System.Web.Mvc;

namespace Blogcatch.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PagesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Admin/Page
        public ActionResult Index()
        {
            var pages = _context.Pages
                .ToArray()
                .OrderBy(p => p.Sorting)
                .Select(p => new PageViewModel(p))
                .ToList();
            return View(pages);
        }

        [HttpGet]
        public ActionResult AddPage()
        {

            return View();
        }
    }
}