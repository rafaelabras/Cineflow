using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace Cineflow.repository
{
    public class DatabaseService
    {
        private readonly string _connectionString;
        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection()
            => new NpgsqlConnection(_connectionString);

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<T>(sql, parameters);
        }

        public async Task<int> ExecuteAsync(string sql, object? parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(sql, parameters);
        }

        public async Task<T> QuerySingleAsync<T>(string sql, object? parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.QuerySingleAsync<T>(sql, parameters);
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object? parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.ExecuteScalarAsync<T>(sql, parameters);
        }

        public async Task<IEnumerable<TReturn>> QueryAsyncMultipleObjectsThreeJoins<TFirst, TSecond, TThrid, TReturn>(string sql,
            Func<TFirst, TSecond, TThrid, TReturn> map,
            string? splitOn,
            object? parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync(sql, map, parameters, splitOn: splitOn);
        }

    }
}
