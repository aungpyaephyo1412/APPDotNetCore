using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace Shared;

public class AdoDotNetService
{
    private readonly string _connectionString;

    public AdoDotNetService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<T> Query<T>(string query, params AdoDotNetParameters[]? parameters)
    {
        var connection = new SqlConnection(_connectionString);
        connection.Open();
        var cmd = new SqlCommand(query, connection);
        if (parameters is not null && parameters.Length > 0)
            cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());
        var sqlDataAdapter = new SqlDataAdapter(cmd);
        var dataTable = new DataTable();
        sqlDataAdapter.Fill(dataTable);
        connection.Close();
        return JsonConvert.DeserializeObject<List<T>>(JsonConvert.SerializeObject(dataTable));
    }

    public T QueryFirstOrDefault<T>(string query, params AdoDotNetParameters[]? parameters)
    {
        var connection = new SqlConnection(_connectionString);
        connection.Open();
        var cmd = new SqlCommand(query, connection);
        if (parameters is not null && parameters.Length > 0)
            cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());
        var sqlDataAdapter = new SqlDataAdapter(cmd);
        var dataTable = new DataTable();
        sqlDataAdapter.Fill(dataTable);
        connection.Close();
        var list = JsonConvert.DeserializeObject<List<T>>(JsonConvert.SerializeObject(dataTable));
        return list[0];
    }

    public int Execute(string query, params AdoDotNetParameters[]? parameters)
    {
        var connection = new SqlConnection(_connectionString);
        connection.Open();
        var cmd = new SqlCommand(query, connection);
        if (parameters is not null && parameters.Length > 0)
            cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());
        var result = cmd.ExecuteNonQuery();
        connection.Close();
        return result;
    }

    public class AdoDotNetParameters
    {
        public AdoDotNetParameters(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }
}