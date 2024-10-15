using Application.Modules.TagsModule.Commands.TagAddCommand;
using Application.Modules.TagsModule.Commands.TagEditCommand;
using Application.Modules.TagsModule.Commands.TagRemoveCommand;
using Application.Modules.TagsModule.Queries.TagGetByIdQuery;
using Application.Modules.TagsModule.Queries.TagsGetAllQuery;
using Application.Modules.TagsModule.Queries.TagsPagedQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Common;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController(IMediator mediator) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("{page:int:min(1)}/{size:int:min(2)}")]
        public async Task<IActionResult> GetAll([FromRoute] TagsPagedRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromRoute] TagsGetAllRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get([FromRoute] TagGetByIdRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(TagAddRequest request)
        {
            request.Text = string.Empty;

            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data,StatusCodes.Status201Created);
            return CreatedAtAction(nameof(Get), new { id = data.Id }, response);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit(int id, [FromBody] TagEditRequest request)
        {
            request.Id = id;
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute] TagRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }
    }
}