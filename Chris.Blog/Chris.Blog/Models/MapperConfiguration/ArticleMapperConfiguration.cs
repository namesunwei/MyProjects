using AutoMapper;
using Chris.Blog.Models.RequestModels;
using Chris.Blog.Models.ViewModels;
using Chris.Blog.Services.DbModels;
using Chris.Blog.Services.DomainModels;

namespace Chris.Blog.Models.MapperConfiguration
{
    public class ArticleMapperConfiguration : Profile
    {
        public ArticleMapperConfiguration()
        {
            CreateMap<ArticleRequest, ArticleDomain>().ReverseMap();

            CreateMap<ArticleDomain, Article>().ReverseMap();

            CreateMap<ArticleDomain, ArticleViewModel>().ReverseMap();

        }
    }
}
