using System.Data;
using System.Data.Common;
using Cineflow.helpers;
using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace Cineflow.repository
{
    public class DatabaseService
    {
        private readonly LogResultHelper<DatabaseService> _logger;
        private readonly string _connectionString;
        public DatabaseService(string connectionString, LogResultHelper<DatabaseService> logger)
        {
            _logger = logger;
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection()
            => new NpgsqlConnection(_connectionString);

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
        {

            try
            {
                using var connection = CreateConnection();
                return await connection.QueryAsync<T>(sql, parameters);
            } catch (DbException db)
            {
                _logger.LogExceptionSql("Erro DB ao fazer a query", parameters, sql, db);
                throw;
            }
        }

        public async Task<int> ExecuteAsync(string sql, object? parameters = null)
        {
            try
            {
                using var connection = CreateConnection();
                return await connection.ExecuteAsync(sql, parameters);
            }  catch (DbException db)
            {
                _logger.LogExceptionSql("Erro DB ao executar", parameters, sql, db);
                throw;
            }
        }

        public async Task<T> QuerySingleAsync<T>(string sql, object? parameters = null)
        {
            try
            {
                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<T>(sql, parameters);
            }
            catch (DbException db)
            {
                _logger.LogExceptionSql("Erro DB ao fazer a query", parameters, sql, db);
                throw;
            }
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object? parameters = null)
        {
            try
            {
                using var connection = CreateConnection();
                return await connection.ExecuteScalarAsync<T>(sql, parameters);
            }
            catch (DbException db)
            {
                _logger.LogExceptionSql("Erro DB ao realizar execute scalar", parameters, sql, db);
                throw;
            }
        }

        public async Task<IEnumerable<TReturn>> QueryAsyncMultipleObjectsOneJoin<TFirst, TSecond, TReturn>(string sql,
            Func<TFirst, TSecond, TReturn> map,
            string? splitOn,
            object? parameters = null)
        {
            try
            {
                using var connection = CreateConnection();
                return await connection.QueryAsync(sql, map, parameters, splitOn: splitOn);
            }
            catch (DbException db)
            {
                _logger.LogExceptionSqlQueryMap("Erro DB ao fazer a query", parameters, sql, map,db);
                throw;
            }
        }
        
        public async Task<IEnumerable<TReturn>> QueryAsyncMultipleObjectsThreeJoins<TFirst, TSecond, TThrid, TReturn>(string sql,
            Func<TFirst, TSecond, TThrid, TReturn> map,
            string? splitOn,
            object? parameters = null)
        {
            try
            {
                using var connection = CreateConnection();
                return await connection.QueryAsync(sql, map, parameters, splitOn: splitOn);
            }
            catch (DbException db)
            {
                _logger.LogExceptionSqlQueryMap("Erro DB ao fazer a query", parameters, sql, map,db);
                throw;
            }
        }

        public async Task<IEnumerable<TReturn>> QueryAsyncMultipleObjectsFourJoins<TFirst, TSecond, TThird, TFourth,
            TReturn>(string sql,
            Func<TFirst, TSecond, TThird, TFourth, TReturn> map,
            string? splitOn,
            object? parameters = null)
        {
            try
            {
                using var connection = CreateConnection();
                return await connection.QueryAsync(sql, map, parameters, splitOn: splitOn);
            }
            catch (DbException db)
            {
                _logger.LogExceptionSqlQueryMap("Erro DB ao fazer a query", parameters, sql, map,db);
                throw;
            }
        }

        public async Task<IEnumerable<TReturn>> QueryAsyncMultipleObjectsSixJoins<TFirst, TSecond, TThird, TFourth,
            TFifth,
            TSixth, TReturn>(string sql,
            Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map,
            string? splitOn,
            object? parameters = null)
        {
            try
            {
                using var connection = CreateConnection();
                return await connection.QueryAsync(sql, map, parameters, splitOn: splitOn);
            }
            catch (DbException db)
            {
                _logger.LogExceptionSqlQueryMap("Erro DB ao fazer a query", parameters, sql, map,db);
                throw;
            }
        }
    }
}
