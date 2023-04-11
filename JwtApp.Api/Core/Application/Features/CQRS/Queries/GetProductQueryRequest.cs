using JwtApp.Api.Core.Application.DTOs;
using MediatR;

namespace JwtApp.Api.Core.Application.Features.CQRS.Queries
{
    public class GetProductQueryRequest : IRequest<ProductListDto>
    {
        public int Id { get; set; }

        public GetProductQueryRequest(int ıd)
        {
            Id = ıd;
        }
    }
}
