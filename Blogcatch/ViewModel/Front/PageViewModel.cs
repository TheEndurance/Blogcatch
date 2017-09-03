using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blogcatch.Areas.Admin.Models;

namespace Blogcatch.ViewModel.Front
{
    public class PageViewModel
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public bool HasSidebar { get; set; }

        public PageViewModel(Page page)
        {
            this.Title = page.Title;
            this.Body = page.Body;
            this.HasSidebar = page.HasSidebar;
        }
    }
}