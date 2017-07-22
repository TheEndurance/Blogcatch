using AutoMapper;
using Blogcatch.Areas.Admin.Models;
using Blogcatch.Areas.Admin.Models.dto;
using Blogcatch.AutoMapper;
using Blogcatch.Models;
using System.Linq;
using System.Web.Http;

namespace Blogcatch.Areas.Admin.Controllers.api
{
    public class BlogController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BlogController()
        {
            _context = new ApplicationDbContext();
            _mapper = WebApiMapper.GetBlogMapper();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // POST : /admin/api/blog/AddCategory
        [HttpPost]
        public IHttpActionResult AddCategory([FromBody]CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (string.IsNullOrWhiteSpace(categoryDto.Name) || categoryDto.Name.Length > 50)
            {
                return BadRequest();
            }

            var category = new Category(categoryDto.Name);

            if (_context.Categories.Any(c => c.Name == category.Name))
            {
                return Json("titleTaken");
            }

            _context.Categories.Add(category);
            _context.SaveChanges();
            return Json(category.Id);
        }


        public IHttpActionResult ReorderCategories([FromBody])
    }
}
