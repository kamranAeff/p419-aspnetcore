namespace WebUI.Models.DTOs.Account
{
    public class AuthenticateResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
