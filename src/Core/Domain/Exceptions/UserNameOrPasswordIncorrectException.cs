namespace Domain.Exceptions
{
    public class UserNameOrPasswordIncorrectException : NotFoundException
    {
        public UserNameOrPasswordIncorrectException() 
            : base("UserName or Passwor is incorrect")
        {
        }
    }
}
