using System.Collections.Generic;
using Blogcatch.Models;

namespace Blogcatch.ViewModel.Front
{
    public class BlogViewModel
    {
        public IEnumerable<BlogPostViewModel> BlogPostViewModels { get; set; }
        public List<string> ActiveWidgets { get; set; }
        public Pager Pager { get; set; }
        public string Search { get; set; }
        public string Category { get; set; }
    }
}