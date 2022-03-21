namespace SimpleBookingWidget.Models
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {

        }

        public ApiResponse(T content)
        {
            Content = content;
            Status = ApiResponseStatus.Success;
        }

        public ApiResponse(T content, string message, ApiResponseStatus status)
        {
            Content = content;
            Message = message;
            Status = status;
        }

        public ApiResponse(string message, ApiResponseStatus status)
        {
            Message = message;
            Status = status;
        }

        public T Content { get; set; }
        public ApiResponseStatus Status { get; set; }
        public string Message { get; set; }
    }

    public enum ApiResponseStatus
    {
        Error = 0,
        Success = 1
    }
}
