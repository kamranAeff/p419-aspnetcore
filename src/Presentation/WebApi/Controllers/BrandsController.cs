using Application.Modules.BrandsModule.Commands.BrandAddCommand;
using Application.Modules.BrandsModule.Commands.BrandEditCommand;
using Application.Modules.BrandsModule.Commands.BrandRemoveCommand;
using Application.Modules.BrandsModule.Queries.BrandsGetAllQuery;
using Application.Modules.BrandsModule.Queries.BrandsPagedQuery;
using Application.Modules.BrandsModule.Queries.BrandGetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Common;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController(IMediator mediator) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BrandsGetAllRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [HttpGet("{page:int:min(1)}/{size:int:min(2)}")]
        public async Task<IActionResult> GetAll([FromQuery] BrandsPagedRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get([FromRoute] BrandGetByIdRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BrandAddRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data, StatusCodes.Status201Created);
            return CreatedAtAction(nameof(Get), new { id = data.Id }, response);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit(int id, [FromBody] BrandEditRequest request)
        {
            request.Id = id;
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute] BrandRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }
    }
}
