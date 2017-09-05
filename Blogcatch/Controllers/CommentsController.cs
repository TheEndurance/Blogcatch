using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogcatch.Controllers
{
    public class CommentsController : Controller
    {
        // GET: Comments
        public ActionResult AddComment()
        {
            return View();
        }
    }
}