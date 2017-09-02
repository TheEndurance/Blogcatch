using System.Collections.Generic;

namespace Blogcatch.ViewModel.Front
{
    public class BlogViewModel
    {
        public IEnumerable<BlogPostViewModel> BlogPostViewModels { get; set; }
        public List<string> ActiveWidgets { get; set; }
    }
}