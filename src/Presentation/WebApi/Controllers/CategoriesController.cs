using Application.Modules.CategoriesModule.Commands.CategoryAddCommand;
using Application.Modules.CategoriesModule.Commands.CategoryEditCommand;
using Application.Modules.CategoriesModule.Commands.CategoryRemoveCommand;
using Application.Modules.CategoriesModule.Queries.CategoriesGetAllQuery;
using Application.Modules.CategoriesModule.Queries.CategoriesPagedQuery;
using Application.Modules.CategoriesModule.Queries.CategoryGetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Common;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] CategoriesGetAllRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("{page:int:min(1)}/{size:int:min(2)}")]
        public async Task<IActionResult> GetAll(CategoriesPagedRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get([FromRoute] CategoryGetByIdRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [HttpPost]
        [Authorize("categories.add")]
        public async Task<IActionResult> Add(CategoryAddRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data, StatusCodes.Status201Created);
            return CreatedAtAction(nameof(Get), new { id = data.Id }, response);
        }

        [HttpPut("{id:int:min(1)}")]
        [Authorize("categories.edit")]
        public async Task<IActionResult> Edit(int id, [FromBody] CategoryEditRequest request)
        {
            request.Id = id;
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [Authorize("categories.remove")]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute] CategoryRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }
    }
}