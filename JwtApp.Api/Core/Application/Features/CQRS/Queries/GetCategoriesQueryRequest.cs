using JwtApp.Api.Core.Application.DTOs;
using MediatR;

namespace JwtApp.Api.Core.Application.Features.CQRS.Queries
{
    public class GetCategoriesQueryRequest:IRequest<List<CategoryListDto>>
    {

    }
}
