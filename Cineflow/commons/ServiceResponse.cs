namespace Cineflow.commons
{
    public class ServiceResponse<T>
    {
        public bool IsSuccess { get; }
        public string? Message { get; }
        public T? Data { get; }

        public ServiceResponse(bool _IsSuccess, string _Message, T _Data)
        {
          IsSuccess = _IsSuccess;
          Message = _Message;
          Data = _Data;
        }

        public static ServiceResponse<T> Ok(T value)
        {
            return new ServiceResponse<T>(true, null, value);
        }
        public static ServiceResponse<T> Fail(string error)
        {
            return new ServiceResponse<T>(false, error, default);
        }

    }
}
