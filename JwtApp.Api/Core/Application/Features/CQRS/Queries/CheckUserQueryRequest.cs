using JwtApp.Api.Core.Application.DTOs;
using MediatR;

namespace JwtApp.Api.Core.Application.Features.CQRS.Queries
{
    public class CheckUserQueryRequest:IRequest<CheckUserResponseDto>
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
