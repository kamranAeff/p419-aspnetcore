using Application.Modules.ContactPostsModule.Commands.ContactPostApplyCommand;
using Application.Modules.ContactPostsModule.Commands.ContactPostReplyCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        [Authorize("contact-posts.reply")]
        //[SwaggerOperation(Description = "admin cavab gondere bilir", Summary = "Admin Cavab Yazir cavab istifadecinin qeyd etdiyi e-poct addresine gonderilir")]
        //[SwaggerResponse(StatusCodes.Status201Created, "Cavab gonderildi")]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "Muraciet tapilmadi")]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, "Validasiya xetasi")]
        public async Task<IActionResult> Reply(ContactPostReplyRequest request)
        {
            await mediator.Send(request);
            var dto = ApiResponse.Success(StatusCodes.Status201Created, "Your message send");
            return StatusCode(dto.Code, dto);
        }
    }
}
