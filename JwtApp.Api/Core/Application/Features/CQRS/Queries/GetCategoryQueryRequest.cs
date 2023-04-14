using JwtApp.Api.Core.Application.DTOs;
using MediatR;

namespace JwtApp.Api.Core.Application.Features.CQRS.Queries
{
    public class GetCategoryQueryRequest:IRequest<CategoryListDto?>
    {
        public int Id { get; set; }

        public GetCategoryQueryRequest(int id)
        {
            Id = id;
        }
    }
}
