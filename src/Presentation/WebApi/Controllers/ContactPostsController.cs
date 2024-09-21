using Application.Modules.ContactPostsModule.Commands.ContactPostApplyCommand;
using Application.Modules.ContactPostsModule.Commands.ContactPostReplyCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Common;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactPostsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Add(ContactPostApplyRequest request)
        {
            await mediator.Send(request);
            var dto = ApiResponse.Success(StatusCodes.Status201Created, "Your message send");
            return StatusCode(dto.Code, dto);
        }

        [HttpPost("reply")]
        [AllowAnonymous]
        public async Task<IActionResult> Reply(ContactPostReplyRequest request)
        {
            await mediator.Send(request);
            var dto = ApiResponse.Success(StatusCodes.Status201Created, "Your message send");
            return StatusCode(dto.Code, dto);
        }
    }
}
