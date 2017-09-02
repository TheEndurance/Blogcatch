using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blogcatch.Areas.Admin.Models;

namespace Blogcatch.ViewModel.Front
{
    public class CategoriesWidgetViewModel
    {
        public string[] FirstCategoriesList { get; set; }

        public string[] SecondCategoriesList { get; set; }

        public string Type { get; set; }

        public CategoriesWidgetViewModel(Widget widget)
        {
            this.Type = widget.Type;
        }
    }
}