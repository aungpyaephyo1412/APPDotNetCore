using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RestApi.Models;
using Shared;

namespace RestApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BlogAdoDotNet2Controller : ControllerBase
{
    private readonly AdoDotNetService _adoService = new AdoDotNetService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
    [HttpGet]
    public IActionResult GetBlogs()
    {
        string query = "SELECT * FROM Tbl_Blog";
        var list = _adoService.Query<BlogModel>(query);
        return Ok(list);
    }

    [HttpGet("{id}")]
    public IActionResult GetBlog(int id)
    {
        string query = "SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId";
        var item = _adoService.QueryFirstOrDefault<BlogModel>(query,
            new AdoDotNetService.AdoDotNetParameters("@BlogId", id));
        return Ok(item);
    }

    [HttpPost]
    public IActionResult CreateBlog(BlogModel blog)
    {
        SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        string query =
            @"INSERT INTO Tbl_Blog ([BlogTitle],[BlogAuthor],[BlogContent]) VALUES (@blogTitle,@blogAuthor,@blogContent)";
        int result = _adoService.Execute(query,
            new AdoDotNetService.AdoDotNetParameters("@blogTitle", blog.BlogTitle),
            new AdoDotNetService.AdoDotNetParameters("@blogAuthor", blog.BlogAuthor),
            new AdoDotNetService.AdoDotNetParameters("@blogContent", blog.BlogContent)
            );
        string message = result > 0 ? "Insert success" : "Insert Fail";
        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBlog(int id, BlogModel blog)
    {
        string query =
            @"UPDATE Tbl_Blog SET [BlogTitle] = @BlogTitle,[BlogAuthor] = @BlogAuthor,[BlogContent]=@BlogContent WHERE BlogId=@BlogId";
        int result = _adoService.Execute(query,
            new AdoDotNetService.AdoDotNetParameters("@BlogId", id),
            new AdoDotNetService.AdoDotNetParameters("@BlogTitle", blog.BlogTitle),
            new AdoDotNetService.AdoDotNetParameters("@BlogAuthor", blog.BlogAuthor),
            new AdoDotNetService.AdoDotNetParameters("@BlogContent", blog.BlogContent)
        );
        string message = result > 0 ? "Update success" : "Update Fail";
        return Ok(message);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBlog(int id)
    {
        string query = @"DELETE FROM Tbl_Blog WHERE [BlogId]=@BlogId";
        int result = _adoService.Execute(query,
            new AdoDotNetService.AdoDotNetParameters("@BlogId", id)
        );
        string message = result > 0 ? "Delete success" : "Delete Fail";
        return Ok(message);
    }
}