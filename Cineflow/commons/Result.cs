namespace Cineflow.commons
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public string? Error { get; } // erro se trata de uma mensagem tecnica para debug
        public string? ErrorMessageUser { get;  } // mensagem de erro para o user
        public string? SucessMessageUser { get; } // mensagem de sucesso para o user
        public T? Value { get; }

        public Result(bool _IsSuccess, string _Error, T _Value, string? _sucessMessage, string _errorMessage)
        {
            IsSuccess = _IsSuccess;
            Error = _Error;
            Value = _Value;
            SucessMessageUser = _sucessMessage;
            ErrorMessageUser = _errorMessage;
        }

        public static Result<T> Success(T value, string? sucessMessage) =>
            new Result<T>(true, null,value, sucessMessage, null);
        
        public static Result<T> Failure(string error, string? errorMessage) =>
            new Result<T>(false, error, default, null, errorMessage);

    }
}
