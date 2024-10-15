namespace WebApi.Middlewares
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TransactionAttribute : Attribute
    {
    }
}