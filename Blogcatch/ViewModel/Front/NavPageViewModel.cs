using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blogcatch.Areas.Admin.Models;

namespace Blogcatch.ViewModel.Front
{
    public class NavPageViewModel
    {
        public string Title { get; set; }
        public string Slug { get; set; }

        public NavPageViewModel(Page page)
        {
            this.Title = page.Title;
            this.Slug = page.Slug;
        }
    }
}