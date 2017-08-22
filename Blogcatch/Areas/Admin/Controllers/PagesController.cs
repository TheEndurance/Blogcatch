using Blogcatch.Areas.Admin.Models;
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
        [HttpGet]
        // GET: Admin/Pages
        public ActionResult Index()
        {
            var pages = _context.Pages
                .ToArray()
                .OrderBy(p => p.Sorting)
                .Select(p => new PageViewModel(p))
                .ToList();
            return View(pages);
        }

        // GET : Admin/Pages/AddPage
        [HttpGet]
        public ActionResult AddPage()
        {
            var pageVM = new PageViewModel();
            return View("PageForm", pageVM);
        }

        // POST : Admin/Pages/AddPage
        [HttpPost]
        public ActionResult AddPage(PageViewModel pageVM)
        {
            if (!ModelState.IsValid)
            {
                return View("PageForm", pageVM);
            }

            var page = new Page(pageVM);

            if (_context.Pages.Any(p => p.Title == pageVM.Title || p.Slug == pageVM.Slug))
            {
                ModelState.AddModelError("", "Title or slug already exists");
                return View("PageForm", pageVM);
            }

            _context.Pages.Add(page);
            _context.SaveChanges();

            TempData["SM"] = "Page was successfully added!";

            return View("PageForm", pageVM);
        }

        // GET: Admin/Pages/EditPage/Id
        [HttpGet]
        public ActionResult EditPage(int id)
        {
            var page = _context.Pages.SingleOrDefault(p => p.Id == id);
            if (page == null)
            {
                return Content("Page Not Found");
            }
            var pageVM = new PageViewModel(page);
            return View("PageForm", pageVM);
        }

        // POST: Admin/Pages/EditPage
        [HttpPost]
        public ActionResult EditPage(PageViewModel pageVM)
        {
            if (!ModelState.IsValid)
            {
                return View("PageForm", pageVM);
            }

            var page = _context.Pages.SingleOrDefault(p => p.Id == pageVM.Id);
            if (page == null)
            {
                return Content("Page not found");
            }

            Page.EditPage(pageVM, page);

            if (_context.Pages.Where(p => p.Id != pageVM.Id).Any(p => p.Title == pageVM.Title || p.Slug == pageVM.Slug))
            {
                ModelState.AddModelError("", "Title or slug already exists");
                return View("PageForm", pageVM);
            }

            _context.SaveChanges();

            TempData["SM"] = "Page was successfully edited!";

            return View("PageForm", pageVM);
        }

        // GET : Admin/Pages/PageDetails/Id
        [HttpGet]
        public ActionResult PageDetails(int id)
        {
            var page = _context.Pages.SingleOrDefault(p => p.Id == id);
            if (page == null)
            {
                return Content("That page does not exist");
            }
            var pageVM = new PageViewModel(page);
            return View(pageVM);
        }

        // GET : Admin/Pages/EditSidebar
        [HttpGet]
        public ActionResult EditSidebar()
        {
            var sidebar = _context.Sidebar.Find(1);
            var sidebarVM = new SidebarViewModel(sidebar);
            return View(sidebarVM);
        }

        // POST : Admin/Pages/EditSidebar
        [HttpPost]
        public ActionResult EditSidebar(SidebarViewModel sidebarVM)
        {
            var sidebar = _context.Sidebar.Find(1);
            sidebar.Body = sidebarVM.Body;
            _context.SaveChanges();
            TempData["SM"] = "Sidebar successfully edited!";
            return View(sidebarVM);
        }


    }
}