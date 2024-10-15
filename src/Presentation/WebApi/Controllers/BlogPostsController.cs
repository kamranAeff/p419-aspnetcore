using Application.Modules.BlogPostsModule.Commands.BlogPostAddCommand;
using Application.Modules.BlogPostsModule.Commands.BlogPostEditCommand;
using Application.Modules.BlogPostsModule.Commands.BlogPostRemoveCommand;
using Application.Modules.BlogPostsModule.Queries.BlogPostGetByIdQuery;
using Application.Modules.BlogPostsModule.Queries.BlogPostGetBySlugQuery;
using Application.Modules.BlogPostsModule.Queries.BlogPostsGetAllQuery;
using Application.Modules.BlogPostsModule.Queries.BlogPostsGetPopularsQuery;
using Application.Modules.BlogPostsModule.Queries.BlogPostsPagedQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Common;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] BlogPostsGetAllRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{page:int:min(1)}/{size:int:min(2)}")]
        public async Task<IActionResult> GetAll([FromQuery] BlogPostsPagedRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get([FromRoute] BlogPostGetByIdRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("populars/{recordCount:int:min(2)}")]
        public async Task<IActionResult> GetPopulars([FromRoute] BlogPostsGetPopularsRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{slug:minlength(3):slug}")]
        public async Task<IActionResult> GetBySlug([FromRoute] BlogPostGetBySlugRequest request)
        {
            var response = await mediator.Send(request);
            var dto = ApiResponse.Success(response);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] BlogPostAddRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data, StatusCodes.Status201Created);
            return CreatedAtAction(nameof(Get), new { id = data.Id }, response);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit(int id, [FromForm] BlogPostEditRequest request)
        {
            request.Id = id;
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute] BlogPostRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }
    }
}