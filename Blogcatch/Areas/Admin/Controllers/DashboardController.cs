using System.Web.Mvc;
using Blogcatch.Models;

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

            return View();
        }
    }
}