namespace WebUI.Models.Common
{
    public class ApiResponse
    {
        public int Code { get; set; }

        public bool IsSuccess { get; set; }

        public string? Message { get; set; }

        public Dictionary<string, IEnumerable<string>>? Errors { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T? Data { get; set; }
    }
}
