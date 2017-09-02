using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blogcatch.Areas.Admin.Models;

namespace Blogcatch.ViewModel.Front
{
    public class SearchWidgetViewModel
    {
        public string Type { get; set; }

        public SearchWidgetViewModel(Widget widget)
        {
            this.Type = widget.Type;
        }
    }
}