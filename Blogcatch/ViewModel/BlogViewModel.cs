using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogcatch.ViewModel
{
    public class BlogViewModel
    {
        public IEnumerable<BlogPostViewModel> BlogPostViewModels { get; set; }
        public List<string> ActiveWidgets { get; set; }
    }
}