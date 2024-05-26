using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RestApi.Models;

namespace RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogAdoDotNetController : ControllerBase
{
    [HttpGet]
    public IActionResult GetBlogs()
    {
        var connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        var query = "SELECT * FROM Tbl_Blog";
        var cmd = new SqlCommand(query, connection);
        var sqlDataAdapter = new SqlDataAdapter(cmd);
        var dataTable = new DataTable();
        sqlDataAdapter.Fill(dataTable);
        connection.Close();
        var list = dataTable.AsEnumerable().Select(row => new BlogModel
        {
            BlogId = Convert.ToInt32(row["BlogId"]),
            BlogTitle = Convert.ToString(row["BlogTitle"]),
            BlogAuthor = Convert.ToString(row["BlogAuthor"]),
            BlogContent = Convert.ToString(row["BlogContent"])
        }).ToList();
        return Ok(list);
    }

    [HttpGet("{id}")]
    public IActionResult GetBlog(int id)
    {
        var connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        var query = "SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId";
        var cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        var sqlDataAdapter = new SqlDataAdapter(cmd);
        var dataTable = new DataTable();
        sqlDataAdapter.Fill(dataTable);
        connection.Close();
        if (dataTable.Rows.Count == 0) return NotFound("Data not found");

        var dr = dataTable.Rows[0];
        var item = new BlogModel
        {
            BlogId = Convert.ToInt32(dr["BlogId"]),
            BlogTitle = Convert.ToString(dr["BlogTitle"]),
            BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            BlogContent = Convert.ToString(dr["BlogContent"])
        };
        return Ok(item);
    }

    [HttpPost]
    public IActionResult CreateBlog(BlogModel blog)
    {
        var connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        var query =
            @"INSERT INTO Tbl_Blog ([BlogTitle],[BlogAuthor],[BlogContent]) VALUES (@blogTitle,@blogAuthor,@blogContent)";
        var cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@blogTitle", blog.BlogTitle);
        cmd.Parameters.AddWithValue("@blogAuthor", blog.BlogAuthor);
        cmd.Parameters.AddWithValue("@blogContent", blog.BlogContent);
        var result = cmd.ExecuteNonQuery();
        var message = result > 0 ? "Insert success" : "Insert Fail";
        connection.Close();
        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBlog(int id, BlogModel blog)
    {
        var connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        var query =
            @"UPDATE Tbl_Blog SET [BlogTitle] = @BlogTitle,[BlogAuthor] = @BlogAuthor,[BlogContent]=@BlogContent WHERE BlogId=@BlogId";
        var cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
        cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
        cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
        var result = cmd.ExecuteNonQuery();
        var message = result > 0 ? "Update success" : "Update Fail";
        connection.Close();
        return Ok(message);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBlog(int id)
    {
        var connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        var query = @"DELETE FROM Tbl_Blog WHERE [BlogId]=@BlogId";
        var cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        var result = cmd.ExecuteNonQuery();
        connection.Close();
        if (result > 0) return NoContent();

        return StatusCode(400, "Something went wrong!");
    }
}