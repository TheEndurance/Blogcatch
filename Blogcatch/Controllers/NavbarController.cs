using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogcatch.Models;
using Blogcatch.ViewModel.Front;

namespace Blogcatch.Controllers
{
    public class NavbarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NavbarController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Navbar
        public ActionResult Navbar()
        {
            var navPageVM = _context.Pages.OrderBy(x=>x.Sorting).ToList().Select(x => new NavPageViewModel(x));
            return PartialView("_Navbar", navPageVM);
        }
    }
}