using Application.Modules.AccountModule.Commands.SignInCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Common;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IMediator mediator) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SingIn([FromBody] SignInRequest request)
        {
            var response = await mediator.Send(request);
            var dto = ApiResponse.Success(response);
            return Ok(dto);
        }
    }
}
