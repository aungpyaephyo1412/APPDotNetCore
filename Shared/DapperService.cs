using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Shared;

public class DapperService
{
    private readonly string _connectionString;

    public DapperService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<T> Query<T>(string query, object? param = null)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        return db.Query<T>(query, param).ToList();
    }

    public T QueryFirstOrDefault<T>(string query, object? param = null)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        return db.Query<T>(query, param).FirstOrDefault()!;
    }

    public int Execute(string query, object? param = null)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        return db.Execute(query, param);
    }
}