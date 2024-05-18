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
        SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        string query = "SELECT * FROM Tbl_Blog";
        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
        DataTable dataTable = new DataTable();
        sqlDataAdapter.Fill(dataTable);
        connection.Close();
        List<BlogModel> list = dataTable.AsEnumerable().Select(row =>new BlogModel
        {
            BlogId = Convert.ToInt32(row["BlogId"]),
            BlogTitle = Convert.ToString(row["BlogTitle"]),
            BlogAuthor = Convert.ToString(row["BlogAuthor"]),
            BlogContent = Convert.ToString(row["BlogContent"]),
        } ).ToList();
        return Ok(list);
    }

    [HttpGet("{id}")]
    public IActionResult GetBlog(int id)
    {
        SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        string query = "SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId";
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId",id);
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
        DataTable dataTable = new DataTable();
        sqlDataAdapter.Fill(dataTable);
        connection.Close();
        if (dataTable.Rows.Count == 0)
        {
            return NotFound("Data not found");
        }

        DataRow dr = dataTable.Rows[0];
        var item = new BlogModel
        {
            BlogId = Convert.ToInt32(dr["BlogId"]),
            BlogTitle = Convert.ToString(dr["BlogTitle"]),
            BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            BlogContent = Convert.ToString(dr["BlogContent"]),
        };
        return Ok(item);
    }

    [HttpPost]
    public IActionResult CreateBlog(BlogModel blog)
    {
        SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        string query =
            @"INSERT INTO Tbl_Blog ([BlogTitle],[BlogAuthor],[BlogContent]) VALUES (@blogTitle,@blogAuthor,@blogContent)";
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@blogTitle", blog.BlogTitle);
        cmd.Parameters.AddWithValue("@blogAuthor", blog.BlogAuthor);
        cmd.Parameters.AddWithValue("@blogContent", blog.BlogContent);
        int result = cmd.ExecuteNonQuery();
        string message = result > 0 ? "Insert success" : "Insert Fail";
        connection.Close();
        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBlog(int id, BlogModel blog)
    {
        SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        string query =
            @"UPDATE Tbl_Blog SET [BlogTitle] = @BlogTitle,[BlogAuthor] = @BlogAuthor,[BlogContent]=@BlogContent WHERE BlogId=@BlogId";
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
        cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
        cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
        int result = cmd.ExecuteNonQuery();
        string message = result > 0 ? "Update success" : "Update Fail";
        connection.Close();
        return Ok(message);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBlog(int id)
    {
        SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        string query = @"DELETE FROM Tbl_Blog WHERE [BlogId]=@BlogId";
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        int result = cmd.ExecuteNonQuery();
        connection.Close();
        if (result>0)
        {
            return NoContent();
        }

        return StatusCode(400, "Something went wrong!");
    }
}