using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogcatch.Models;

namespace Blogcatch.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;


        public BlogController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Blog
        public ActionResult Index()
        {
            var posts = _context.Posts.ToList();
            return View();

        }
    }
}