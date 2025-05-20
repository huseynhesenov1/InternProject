namespace Project.BL.Models
{

    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        // ?lav? olunur
        public string ErrorDetail { get; set; }

        public static ApiResponse<T> Success(T data, string message = "")
        {
            return new ApiResponse<T> { IsSuccess = true, Data = data, Message = message };
        }

        public static ApiResponse<T> Fail(string message)
        {
            return new ApiResponse<T> { IsSuccess = false, Message = message };
        }

        public static ApiResponse<T> Fail(string message, string detail)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Message = message,
                ErrorDetail = detail
            };
        }
    }

    //public class ApiResponse<T>
    //{
    //    public bool IsSuccess { get; set; }
    //    public string Message { get; set; }
    //    public string Error { get; set; }
    //    public T Data { get; set; }

    //    public static ApiResponse<T> Success(T data, string message = "Success")
    //    {
    //        return new ApiResponse<T>
    //        {
    //            IsSuccess = true,
    //            Message = message,
    //            Error = string.Empty,
    //            Data = data
    //        };
    //    }

    //    public static ApiResponse<T> Fail(string error, string message = "Failed")
    //    {
    //        return new ApiResponse<T>
    //        {
    //            IsSuccess = false,
    //            Message = message,
    //            Error = error,
    //            Data = default
    //        };
    //    }
    //}

}