using System.Data;
using System.Data.SqlClient;

namespace ConsoleApp.AdoDotNetExamples;

internal class AdoDotNetExample
{
    private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new()
    {
        DataSource = ".", /* Server Name */
        InitialCatalog = "DotNetSLH",
        UserID = "sa",
        Password = "Typle@14122003"
    };

    public void Read()
    {
        var connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        Console.WriteLine("Connection Open.");

        var query = "SELECT * FROM Tbl_Blog";
        var cmd = new SqlCommand(query, connection);
        var sqlDataAdapter = new SqlDataAdapter(cmd);
        var dataTable = new DataTable();
        sqlDataAdapter.Fill(dataTable);
        connection.Close();
        Console.WriteLine("Connection Close.");

        foreach (DataRow dataRow in dataTable.Rows)
        {
            Console.WriteLine(dataRow["BlogId"]);
            Console.WriteLine(dataRow["BlogTitle"]);
            Console.WriteLine(dataRow["BlogAuthor"]);
            Console.WriteLine(dataRow["BlogContent"]);
        }
    }

    public void Create(string title, string author, string content)
    {
        var connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        Console.WriteLine("Connection Open.");

        var query =
            @"INSERT INTO Tbl_Blog ([BlogTitle],[BlogAuthor],[BlogContent]) VALUES (@blogTitle,@blogAuthor,@blogContent)";
        var cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@blogTitle", title);
        cmd.Parameters.AddWithValue("@blogAuthor", author);
        cmd.Parameters.AddWithValue("@blogContent", content);
        var result = cmd.ExecuteNonQuery();
        var message = result > 0 ? "Insert success" : "Insert Fail";

        connection.Close();
        Console.WriteLine("Connection Close.");
        Console.WriteLine(message);
    }

    public void Edit(int id)
    {
        var connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        Console.WriteLine("Connection Open.");

        var query = "SELECT * FROM Tbl_Blog";
        var cmd = new SqlCommand(query, connection);
        var sqlDataAdapter = new SqlDataAdapter(cmd);
        var dataTable = new DataTable();
        sqlDataAdapter.Fill(dataTable);
        connection.Close();
        Console.WriteLine("Connection Close.");
        if (dataTable.Rows.Count == 0)
        {
            Console.WriteLine("Empty data");
            return;
        }

        var dataRow = dataTable.Rows[0];
        Console.WriteLine(dataRow["BlogId"]);
        Console.WriteLine(dataRow["BlogTitle"]);
        Console.WriteLine(dataRow["BlogAuthor"]);
        Console.WriteLine(dataRow["BlogContent"]);
    }

    public void Update(int id, string title, string author, string content)
    {
        var connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        Console.WriteLine("Connection Open.");

        var query =
            @"UPDATE Tbl_Blog SET [BlogTitle] = @BlogTitle,[BlogAuthor] = @BlogAuthor,[BlogContent]=@BlogContent WHERE BlogId=@BlogId";
        var cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        cmd.Parameters.AddWithValue("@BlogTitle", title);
        cmd.Parameters.AddWithValue("@BlogAuthor", author);
        cmd.Parameters.AddWithValue("@BlogContent", content);
        var result = cmd.ExecuteNonQuery();
        var message = result > 0 ? "Update success" : "Update Fail";

        connection.Close();
        Console.WriteLine("Connection Close.");
        Console.WriteLine(message);
    }

    public void Delete(int id)
    {
        var connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        Console.WriteLine("Connection Open.");

        var query = @"DELETE FROM Tbl_Blog WHERE [BlogId]=@BlogId";
        var cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        var result = cmd.ExecuteNonQuery();
        var message = result > 0 ? "Delete success" : "Delete Fail";

        connection.Close();
        Console.WriteLine("Connection Close.");
        Console.WriteLine(message);
    }
}