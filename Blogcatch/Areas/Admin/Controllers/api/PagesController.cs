using AutoMapper;
using Blogcatch.Areas.Admin.Models.dto;
using Blogcatch.AutoMapper;
using Blogcatch.Models;
using System.Linq;
using System.Web.Http;

namespace Blogcatch.Areas.Admin.Controllers.api
{
    public class PagesController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PagesController()
        {
            _context = new ApplicationDbContext();
            _mapper = WebApiMapper.GetPagesMapper();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // DELETE : /Admin/api/Pages/id

        [HttpDelete]
        public IHttpActionResult DeletePage([FromUri]int id)
        {
            var page = _context.Pages.SingleOrDefault(p => p.Id == id);
            if (page == null)
            {
                return BadRequest();
            }
            _context.Pages.Remove(page);
            _context.SaveChanges();
            return Ok(_mapper.Map<PageDto>(page));
        }


        [HttpPost]
        public IHttpActionResult ReorderPages([FromBody]int[] id)
        {
            var count = 0;
            foreach (var pageId in id)
            {
                var page = _context.Pages.Single(p => p.Id == pageId);
                page.Sorting = count;
                _context.SaveChanges();

                count++;

            }
            return Ok();
        }
    }
}
