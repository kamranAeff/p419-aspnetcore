using Application.Modules.ColorsModule.Commands.ColorAddCommand;
using Application.Modules.ColorsModule.Commands.ColorEditCommand;
using Application.Modules.ColorsModule.Commands.ColorRemoveCommand;
using Application.Modules.ColorsModule.Queries.ColorsGetAllQuery;
using Application.Modules.ColorsModule.Queries.ColorsPagedQuery;
using Application.Modules.ColorsModule.Queries.ColorGetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Common;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController(IMediator mediator) : ControllerBase
    {

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] ColorsGetAllRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{page:int:min(1)}/{size:int:min(2)}")]
        public async Task<IActionResult> GetAll([FromQuery] ColorsPagedRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get([FromRoute] ColorGetByIdRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [HttpPost]
        [Authorize("colors.add")]
        public async Task<IActionResult> Add(ColorAddRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data, StatusCodes.Status201Created);
            return CreatedAtAction(nameof(Get), new { id = data.Id }, response);
        }

        [Authorize("colors.edit")]
        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ColorEditRequest request)
        {
            request.Id = id;
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [Authorize("colors.remove")]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute] ColorRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }
    }
}
