using Application.Modules.CategoriesModule.Commands.CategoryAddCommand;
using Application.Modules.CategoriesModule.Commands.CategoryEditCommand;
using Application.Modules.CategoriesModule.Commands.CategoryRemoveCommand;
using Application.Modules.CategoriesModule.Queries.CategoriesGetAllQuery;
using Application.Modules.CategoriesModule.Queries.CategoriesPagedQuery;
using Application.Modules.CategoriesModule.Queries.CategoryGetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(IMediator mediator) : ControllerBase
    {
        [HttpPost("{page:int:min(1)}/{size:int:min(2)}")]
        public async Task<IActionResult> GetAll(CategoriesPagedRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CategoriesGetAllRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> GetById([FromRoute] CategoryGetByIdRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddRequest request)
        {
            var response = await mediator.Send(request);

            return Ok(response);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CategoryEditRequest request)
        {
            request.Id = id;
            var response = await mediator.Send(request);

            return Ok(response);
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute] CategoryRemoveRequest request)
        {
            await mediator.Send(request);

            return NoContent();
        }
    }
}
