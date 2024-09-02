namespace Domain.Exceptions
{
    public class UserNameOrPasswordIncorrectException : NotFoundException
    {
        public UserNameOrPasswordIncorrectException() 
            : base("UserName or Password is incorrect")
        {
        }
    }
}
