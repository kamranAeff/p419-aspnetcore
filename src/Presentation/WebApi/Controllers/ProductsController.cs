using Application.Modules.ProductsModule.Commands.ProductAddCommand;
using Application.Modules.ProductsModule.Commands.ProductEditCommand;
using Application.Modules.ProductsModule.Commands.ProductRemoveCommand;
using Application.Modules.ProductsModule.Queries.ProductGetByIdQuery;
using Application.Modules.ProductsModule.Queries.ProductGetBySlugQuery;
using Application.Modules.ProductsModule.Queries.ProductsGetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] ProductsGetAllRequest request)
        {
            var response = await mediator.Send(request);
            var dto = ApiResponse.Success(response);
            return Ok(dto);
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetById([FromRoute] ProductGetByIdRequest request)
        {
            var response = await mediator.Send(request);
            var dto = ApiResponse.Success(response);
            return Ok(dto);
        }

        [HttpGet("{slug:minlength(3):slug}")]
        public async Task<IActionResult> GetBySlug([FromRoute] ProductGetBySlugRequest request)
        {
            var response = await mediator.Send(request);
            var dto = ApiResponse.Success(response);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] ProductAddRequest request)
        {
            var response = await mediator.Send(request);
            var dto = ApiResponse.Success(response);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, dto);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit(int id, [FromForm] ProductEditRequest request)
        {
            request.Id = id;
            var response = await mediator.Send(request);
            var dto = ApiResponse.Success(response);
            return Ok(dto);
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute] ProductRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }
    }
}
