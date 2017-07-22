using AutoMapper;
using Blogcatch.Areas.Admin.Models;
using Blogcatch.Areas.Admin.Models.dto;

namespace Blogcatch.AutoMapper
{
    public static class WebApiMapper
    {
        public static IMapper GetPagesMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Page, PageDto>());
            var mapper = config.CreateMapper();
            return mapper;
        }

        public static IMapper GetBlogMapper()
        {
            var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<CategoryDto, Category>()
            );
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}