using MediatR;

namespace Application.Modules.AccountModule.Commands.SignupCommand
{
    public class SignupRequest : IRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool SubscribeForNews { get; set; }
    }
}
