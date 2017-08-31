using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogcatch.Models;
using Blogcatch.ViewModel;

namespace Blogcatch.Areas.Admin.Controllers
{
    public class WidgetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WidgetsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Admin/Widgets
        public ActionResult Index()
        {
            var widgetVM = _context.Widgets.ToArray().Select(x=>new WidgetViewModel(x)).ToList();
            return View(widgetVM);
        }
    }
}