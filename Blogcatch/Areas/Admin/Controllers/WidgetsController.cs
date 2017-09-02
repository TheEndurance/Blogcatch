using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Blogcatch.Models;
using Blogcatch.ViewModel;
using Blogcatch.ViewModel.Admin;

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
            var widgetVM = _context.Widgets.ToArray().OrderBy(x=>x.Sorting).Select(x=>new WidgetViewModel(x)).ToList();
            return View(widgetVM);
        }

        // POST: Admin/Widgets/ReorderAndEnableWidgets
        [System.Web.Http.HttpPost]
        public ActionResult ReorderAndEnableWidgets(int[] id)
        {
            //Disable all widgets
            var allWidgets = _context.Widgets.ToList();
            foreach (var widget in allWidgets)
            {
                widget.Enabled = false;
            }

            short count = 0;
            if (id != null)
            {
                foreach (var widgetId in id)
                {
                    var widget = _context.Widgets.Find(widgetId);
                    widget.Sorting = count;
                    widget.Enabled = true;
                    count++;
                }
            }
            _context.SaveChanges();
            return Json(new {success = true});
        }
    }
}