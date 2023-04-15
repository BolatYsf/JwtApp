using JwtApp.Api.Core.Application.DTOs;
using JwtApp.Api.Core.Application.Features.CQRS.Commands;
using JwtApp.Api.Core.Application.Features.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtApp.Api.Controllers
{
    [Authorize(Roles ="Admin,Member")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetListProduct()
        {
            var result = await _mediator.Send(new GettAllProductsQueryRequest());
            return Ok(result);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await _mediator.Send(new GetProductQueryRequest(id));
            return result == null ? NotFound() : Ok(result);

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _mediator.Send(new DeleteProductCommandRequest(id));

            return NoContent();
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateProductCommandRequest request)
        {
            await _mediator.Send(request);
            return Created("",request);
        }
        [HttpPut]

        public async Task<IActionResult> Update(UpdateProductCommandRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }
    }
}
