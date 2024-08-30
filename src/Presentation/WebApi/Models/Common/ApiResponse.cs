namespace WebApi.Models.Common
{
    public class ApiResponse
    {
        public int Code { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public Dictionary<string, IEnumerable<string>>? Errors { get; set; }

        public static ApiResponse Success(int statusCode = StatusCodes.Status200OK, string? message = null)
        {
            return new ApiResponse
            {
                Code = statusCode,
                IsSuccess = true,
                Message = message
            };
        }


        public static ApiResponse Success<T>(T data, int statusCode = StatusCodes.Status200OK, string? message = null)
        {
            return new ApiResponse<T>
            {
                Code = statusCode,
                IsSuccess = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse Fail(int statusCode, Dictionary<string, IEnumerable<string>> errors, string? message = null)
        {
            return new ApiResponse
            {
                Code = statusCode,
                IsSuccess = false,
                Message = message,
                Errors = errors
            };
        }

        public static ApiResponse Fail(int statusCode, string message)
        {
            return new ApiResponse
            {
                Code = statusCode,
                IsSuccess = false,
                Message = message
            };
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T? Data { get; set; }
    }
}
