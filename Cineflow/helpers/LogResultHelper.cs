using System.Text;

namespace Cineflow.helpers;

public class LogResultHelper<T>
{
    ILogger<T> _logger;
    
    public LogResultHelper(ILogger<T> logger)
    {
        _logger = logger;
    }

    public void LogResultFiltro(string acao, object filter, string resultado)
    {
        _logger.LogInformation("[API LOG] {Acao} | Filtro={Filtro} | Resultado={Resultado}", acao, filter, resultado);
    }

    public void LogResultRestOperations(string acao, string resultado, object info, string? resultadoInfo = null, LogLevel level = LogLevel.Information )
    {
        switch (level)
        {
            case LogLevel.Information:
                if (resultadoInfo == null)
                    _logger.LogInformation("[API LOG] {Acao} | Resultado: {Resultado}| Info: {Info}", acao, resultado, info);
                else
                    _logger.LogInformation("[API LOG] {Acao} | Resultado: {Resultado} | ResultadoInfo: {resultadoInfo} | Info: {Info}", acao, resultado, resultadoInfo, info);
                break;
            
            case LogLevel.Warning:
                _logger.LogWarning("[API LOG] {Acao} | Resultado: {Resultado} | ResultadoInfo: {resultadoInfo} | Info: {Info}", acao, resultado, resultadoInfo, info);
                break;
            
            case LogLevel.Error:
                _logger.LogError("[API LOG] {Acao} | Resultado: {Resultado} | ResultadoInfo: {resultadoInfo} | Info: {Info}", acao, resultado, resultadoInfo, info);
                break;
        }
    }

    public void LogExceptionSql(string acao, object parametros, string sql, Exception ex)
    {
        _logger.LogError($"[API LOG] {acao} | Parametros: {parametros} | Sql: {sql} | Exception: {ex.Message}", ex);
    }
    
    public void LogExceptionSqlQueryMap(string acao, object parametros, string sql, object map, Exception ex)
    {
        _logger.LogError($"[API LOG] {acao} | Parametros: {parametros} | Map: {map}| Sql: {sql} | Exception: {ex.Message}", ex);
    }
    
    public void LogResultSqlOperations(StringBuilder sql, object parameters, int? rowsAffected = null, LogLevel level = LogLevel.Information, string? sqlResult = null)
    {
        switch (level)
        {
            case LogLevel.Information:
                if (rowsAffected == null) {
                    if (sqlResult == null)
                    {
                        _logger.LogInformation("[API LOG] Sql: {sql} | Parametros: {Params} ", sql.ToString(), parameters);
                        break;
                    }
                    _logger.LogInformation("[API LOG] Sql: {sql} | Parametros: {Params} | SqlResult: {SQLResult}", sql.ToString(), parameters, sqlResult);
                    break;
                }
                _logger.LogInformation("[API LOG] Sql: {sql} | Parametros: {Params} | SqlResult: {SQLResult} | RowsAffected: {RowsAffected}", sql.ToString(), parameters, sqlResult, rowsAffected);
                break;
            
            case LogLevel.Warning:
                if (rowsAffected == null) {
                    if (sqlResult == null)
                    {
                        _logger.LogWarning("[API LOG] Sql: {sql} | Parametros: {Params} ", sql.ToString(), parameters);
                        break;
                    }
                    _logger.LogWarning("[API LOG] Sql: {sql} | Parametros: {Params} | SqlResult: {SQLResult}", sql.ToString(), parameters, sqlResult);
                    break;
                }
                _logger.LogWarning("[API LOG] Sql: {sql} | Parametros: {Params} | SqlResult: {SQLResult} | RowsAffected: {RowsAffected}", sql.ToString(), parameters, sqlResult, rowsAffected);
                break;
            
            case LogLevel.Error:
                if (rowsAffected == null) {
                    if (sqlResult == null)
                    {
                        _logger.LogError("[API LOG] Sql: {sql} | Parametros: {Params} ", sql.ToString(), parameters);
                        break;
                    }
                    _logger.LogError("[API LOG] Sql: {sql} | Parametros: {Params} | SqlResult: {SQLResult}", sql.ToString(), parameters, sqlResult);
                    break;
                }
                _logger.LogError("[API LOG] Sql: {sql} | Parametros: {Params} | SqlResult: {SQLResult} | RowsAffected: {RowsAffected}", sql.ToString(), parameters, sqlResult, rowsAffected);
                break;
        }
}
    
}