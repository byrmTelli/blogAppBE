using System.Text.Json.Serialization;
using blogAppBE.CORE.Enums;
using blogAppBE.CORE.ViewModels;

namespace blogAppBE.CORE.Generics
{
    public class Response<T> where T: class
    {
        public T Data { get; set; }
        public StatusCode StatusCode { get; set; }
        public ErrorViewModel Errors { get; set; }
        [JsonIgnore]
        public bool IsSuccessfull { get; set; }

        public static Response<T> Success(StatusCode statusCode)
        {
            return new Response<T>{Data = default, StatusCode = statusCode,IsSuccessfull = true};
        }

        public static Response<T> Success(T data, StatusCode statusCode)
        {
            return new Response<T>{Data = data, StatusCode = statusCode, IsSuccessfull = true};
        }

        public static Response<T> Fail(string errorMessage,StatusCode statusCode)
        {
            var errorViewModel = new ErrorViewModel(errorMessage);
            return new Response<T>{Errors = errorViewModel, StatusCode = statusCode,IsSuccessfull = false};
        }

        public static Response<T> Fail(List<string> errorMessages,StatusCode statusCode)
        {
            var _errors = new ErrorViewModel
            {
                Errors = errorMessages
            };
            return new Response<T> { Errors = _errors, StatusCode = statusCode };
        }
    }

}