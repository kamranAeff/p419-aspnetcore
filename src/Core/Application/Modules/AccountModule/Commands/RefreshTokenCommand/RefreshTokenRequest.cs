using MediatR;

namespace Application.Modules.AccountModule.Commands.RefreshTokenCommand
{
    public class RefreshTokenRequest : IRequest<AuthenticateResponse>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
