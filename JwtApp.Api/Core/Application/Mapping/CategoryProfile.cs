using AutoMapper;
using JwtApp.Api.Core.Application.DTOs;
using JwtApp.Api.Core.Domain;

namespace JwtApp.Api.Core.Application.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category,CategoryListDto>().ReverseMap();
        }
    }
}
