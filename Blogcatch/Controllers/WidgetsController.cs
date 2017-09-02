using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogcatch.Models;
using Blogcatch.ViewModel;

namespace Blogcatch.Controllers
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

        // GET: Widgets/Search
        public ActionResult Search()
        {
            var search = _context.Widgets.Single(x => x.Id == 1);
            var widgetVM = new WidgetViewModel(search);
            return PartialView("_Search",widgetVM);
        }

    }
}