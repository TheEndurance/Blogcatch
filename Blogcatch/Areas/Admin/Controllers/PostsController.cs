using Blogcatch.Models;
using Blogcatch.ViewModel;
using System.Linq;
using System.Web.Mvc;

namespace Blogcatch.Areas.Admin.Controllers
{



    public class PostsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public PostsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Admin/Posts
        public ActionResult Index()
        {
            var postVMList = _context.Posts.ToArray().Select(x => new PostViewModel(x)).OrderBy(p => p.PostDate)
                .ToList();
            return View(postVMList);
        }

        public ActionResult AddPost()
        {
            var postVM = new PostViewModel();
            return View("PostForm",postVM);
        }
    }
}