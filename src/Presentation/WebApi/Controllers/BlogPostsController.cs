using Application.Modules.BlogPostsModule.Commands.BlogPostAddCommand;
using Application.Modules.BlogPostsModule.Commands.BlogPostEditCommand;
using Application.Modules.BlogPostsModule.Commands.BlogPostRemoveCommand;
using Application.Modules.BlogPostsModule.Queries.BlogPostGetByIdQuery;
using Application.Modules.BlogPostsModule.Queries.BlogPostsGetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BlogPostsGetAllRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<IActionResult> Get([FromRoute]BlogPostGetByIdRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);

            //var response = mapper.Map<BlogPostDto>(data, cfg =>
            //{
            //    if (Request.Headers.TryGetValue("dateFormat", out StringValues formats))
            //        cfg.Items.Add("dateFormat", formats.FirstOrDefault());

            //    string host = $"{Request.Scheme}://{Request.Host}";
            //    cfg.Items.Add("host", host);

            //});
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm]BlogPostAddRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data, StatusCodes.Status201Created);
            return CreatedAtAction(nameof(Get), new { id = data.Id }, response);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Edit(int id, [FromForm] BlogPostEditRequest request)
        {
            var data = await mediator.Send(request);
            var response = ApiResponse.Success(data);
            return Ok(response);
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Remove([FromRoute]BlogPostRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }
    }
}
