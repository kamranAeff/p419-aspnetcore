namespace WebUI.Proxies
{
    public class ApiResponse<T>
        where T : class
    {
        public T? Data { get; set; }
        public int Code { get; set; }
        public bool IsSuccess { get; set; }
    }
}
