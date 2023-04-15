using JwtApp.Api.Core.Application.Features.CQRS.Commands;
using JwtApp.Api.Core.Application.Features.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtApp.Api.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
          var result= await _mediator.Send(new GetCategoriesQueryRequest());
           return result!=null? Ok(result):NotFound();
           
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetCategoryId(int id) {

            var result=await _mediator.Send(new GetCategoryQueryRequest(id));
            return result != null ? Ok(result):NotFound(id);
        }

        [HttpPost]

        public async Task<IActionResult> CreateCategory(CreateCategoryCommandRequest request)
        {
            var result= await _mediator.Send(request);
            return Created("", result);
        }

        [HttpPut]

        public async Task<IActionResult> UpdateCategory(UpdateCategoryCommandRequest request)
        {
            
            await _mediator.Send(request);
            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _mediator.Send(new DeleteCategoryCommandRequest(id));

            return Ok();
        }

    }
}
