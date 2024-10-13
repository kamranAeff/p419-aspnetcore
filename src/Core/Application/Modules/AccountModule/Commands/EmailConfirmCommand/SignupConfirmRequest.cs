using Application.Services;
using MediatR;

namespace Application.Modules.AccountModule.Commands.EmailConfirmCommand
{
    public class SignupConfirmRequest : IRequest
    {
        public string Token { get; set; }
    }

    public class SignupConfirmRequestHandler(ICryptoService cryptoService) : IRequestHandler<SignupConfirmRequest>
    {
        public async Task Handle(SignupConfirmRequest request, CancellationToken cancellationToken)
        {
            var response = cryptoService.Decrypt(request.Token);
            Console.WriteLine(response);
        }
    }
}
