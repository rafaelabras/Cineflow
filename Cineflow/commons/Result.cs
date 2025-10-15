namespace Cineflow.commons
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public string? Error { get; } // erro se trata de uma mensagem tecnica para debug
        public T? Value { get; }

        public Result(bool _IsSuccess, string _Error, T _Value)
        {
            IsSuccess = _IsSuccess;
            Error = _Error;
            Value = _Value;
        }

        public static Result<T> Success(T value) =>
            new Result<T>(true, null,value);
        
        public static Result<T> Failure(string error) =>
            new Result<T>(false, error, default);

    }
}
