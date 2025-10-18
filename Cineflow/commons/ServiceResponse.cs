using System.Net;

namespace Cineflow.commons
{
    public class ServiceResponse<T>
    {
        public bool IsSuccess { get; }
        public string? ErrorMessageUser { get; } // mensagem de erro para o user
        public string? SucessMessageUser { get; } // mensagem de sucesso para o user
        public T? Data { get; }


        public ServiceResponse(bool _IsSuccess, T _Data, string _ErrorMessageUser, string _SucessMessageUser)
        {
          IsSuccess = _IsSuccess;
          ErrorMessageUser  = _ErrorMessageUser;
          SucessMessageUser = _SucessMessageUser;
          Data = _Data;
        }

        public static ServiceResponse<T> Ok(T value, string sucessMessageUser)
        {
            return new ServiceResponse<T>(true, value, " " ,sucessMessageUser);
        }
        public static ServiceResponse<T> Fail(string errorMessageuser)
        {
            return new ServiceResponse<T>(false, default , errorMessageuser , " ");
        }

    }
}
