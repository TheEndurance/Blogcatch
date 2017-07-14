﻿using Blogcatch.Areas.Admin.Models;
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

            return View("PageForm");
        }

        // POST : Admin/Pages/AddPage
        [HttpPost]
        public ActionResult Create(PageViewModel pageVM)
        {
            if (!ModelState.IsValid)
            {
                return View("PageForm", pageVM);
            }

            var page = Page.CreatePage(pageVM);

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

            return View("PageForm", pageVM);
        }


    }
}