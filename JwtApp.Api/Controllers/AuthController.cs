using JwtApp.Api.Core.Application.Features.CQRS.Commands;
using JwtApp.Api.Core.Application.Features.CQRS.Queries;
using JwtApp.Api.Infrastructure.Tools;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace JwtApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]

        public async Task<IActionResult> Register(RegisterUserCommandRequest request)
        {
            await _mediator.Send(request);
            return Created("", request);
        }

        [HttpPost("[action]")]

        public async Task<IActionResult> Login(CheckUserQueryRequest request)
        {
            var dto=await _mediator.Send(request);
            if (dto.IsExist)
            {
                
                return Created("", JwtGenerator.GenerateToken(dto));
            }
            else
            {
                return BadRequest("user not found");
            }
        }
    }
}
