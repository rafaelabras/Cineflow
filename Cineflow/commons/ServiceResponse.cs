namespace Cineflow.commons
{
    public class ServiceResponse<T>
    {
        public bool IsSuccess { get; }
        public string? Message { get; } = string.Empty;
        public T? Data { get; }

        public ServiceResponse(bool _IsSuccess, string _Message, T _Data)
        {
          IsSuccess = _IsSuccess;
          Message = _Message;
          Data = _Data;
        }

        public static ServiceResponse<T> Ok(T value, string message = " ")
        {
            return new ServiceResponse<T>(true, message, value);
        }
        public static ServiceResponse<T> Fail(string error)
        {
            return new ServiceResponse<T>(false, error, default);
        }

    }
}
