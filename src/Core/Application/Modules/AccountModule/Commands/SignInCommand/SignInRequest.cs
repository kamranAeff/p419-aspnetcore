using MediatR;

namespace Application.Modules.AccountModule.Commands.SignInCommand
{
    public class SignInRequest : IRequest<AuthenticateResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
