using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogcatch.Models;
using Blogcatch.ViewModel;
using Blogcatch.ViewModel.Admin;
using Blogcatch.ViewModel.Front;

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
            var searchWidget = _context.Widgets.Single(x => x.Id == 1);
            var searchWidgetVM = new SearchWidgetViewModel(searchWidget);
            return PartialView("_Search",searchWidgetVM);
        }

        // GET: Widgets/Categories
        public ActionResult Categories()
        {
            var categoriesWidget = _context.Widgets.Single(x => x.Id == 2);
            var categories = _context.Categories.OrderBy(x => x.Sorting).ToArray().Select(x => x.Name).ToArray();

            //split the category lists into two
            var firstCategoryList = categories.Take(categories.Length / 2).ToArray();
            var secondCategoryList = categories.Skip(categories.Length / 2).ToArray();

            var categoriesWidgetVM = new CategoriesWidgetViewModel(categoriesWidget);

            categoriesWidgetVM.FirstCategoriesList = firstCategoryList;
            categoriesWidgetVM.SecondCategoriesList = secondCategoryList;

            return View("_Categories",categoriesWidgetVM);
        }

    }
}