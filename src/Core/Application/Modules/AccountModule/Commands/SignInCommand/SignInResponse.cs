namespace Application.Modules.AccountModule.Commands.SignInCommand
{
    public class SignInResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
