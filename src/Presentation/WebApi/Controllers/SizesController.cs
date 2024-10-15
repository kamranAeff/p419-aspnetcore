using Application.Modules.SizesModule.Commands.SizeAddCommand;
using Application.Modules.SizesModule.Commands.SizeEditCommand;
using Application.Modules.SizesModule.Commands.SizeRemoveCommand;
using Application.Modules.SizesModule.Queries.SizeGetByIdQuery;
using Application.Modules.SizesModule.Queries.SizesGetAllQuery;
using Application.Modules.SizesModule.Queries.SizesPagedQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Common;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] SizesGetAllRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{page:int:min(1)}/{size:int:min(2)}")]
        public async Task<IActionResult> GetAll([FromQuery] SizesPagedRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get([FromRoute] SizeGetByIdRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [HttpPost]
        [Authorize("brands.add")]
        public async Task<IActionResult> Add(SizeAddRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data, StatusCodes.Status201Created);
            return CreatedAtAction(nameof(Get), new { id = data.Id }, response);
        }

        [Authorize("brands.edit")]
        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit(int id, [FromBody] SizeEditRequest request)
        {
            request.Id = id;
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [Authorize("brands.remove")]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute] SizeRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }
    }
}