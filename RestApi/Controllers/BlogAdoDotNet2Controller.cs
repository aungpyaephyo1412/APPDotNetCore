using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RestApi.Models;
using Shared;

namespace RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogAdoDotNet2Controller : ControllerBase
{
    private readonly AdoDotNetService _adoService = new(ConnectionString.SqlConnectionStringBuilder.ConnectionString);

    [HttpGet]
    public IActionResult GetBlogs()
    {
        var query = "SELECT * FROM Tbl_Blog";
        var list = _adoService.Query<BlogModel>(query);
        return Ok(list);
    }

    [HttpGet("{id}")]
    public IActionResult GetBlog(int id)
    {
        var query = "SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId";
        var item = _adoService.QueryFirstOrDefault<BlogModel>(query,
            new AdoDotNetService.AdoDotNetParameters("@BlogId", id));
        return Ok(item);
    }

    [HttpPost]
    public IActionResult CreateBlog(BlogModel blog)
    {
        var connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        var query =
            @"INSERT INTO Tbl_Blog ([BlogTitle],[BlogAuthor],[BlogContent]) VALUES (@blogTitle,@blogAuthor,@blogContent)";
        var result = _adoService.Execute(query,
            new AdoDotNetService.AdoDotNetParameters("@blogTitle", blog.BlogTitle),
            new AdoDotNetService.AdoDotNetParameters("@blogAuthor", blog.BlogAuthor),
            new AdoDotNetService.AdoDotNetParameters("@blogContent", blog.BlogContent)
        );
        var message = result > 0 ? "Insert success" : "Insert Fail";
        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBlog(int id, BlogModel blog)
    {
        var query =
            @"UPDATE Tbl_Blog SET [BlogTitle] = @BlogTitle,[BlogAuthor] = @BlogAuthor,[BlogContent]=@BlogContent WHERE BlogId=@BlogId";
        var result = _adoService.Execute(query,
            new AdoDotNetService.AdoDotNetParameters("@BlogId", id),
            new AdoDotNetService.AdoDotNetParameters("@BlogTitle", blog.BlogTitle),
            new AdoDotNetService.AdoDotNetParameters("@BlogAuthor", blog.BlogAuthor),
            new AdoDotNetService.AdoDotNetParameters("@BlogContent", blog.BlogContent)
        );
        var message = result > 0 ? "Update success" : "Update Fail";
        return Ok(message);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBlog(int id)
    {
        var query = @"DELETE FROM Tbl_Blog WHERE [BlogId]=@BlogId";
        var result = _adoService.Execute(query,
            new AdoDotNetService.AdoDotNetParameters("@BlogId", id)
        );
        var message = result > 0 ? "Delete success" : "Delete Fail";
        return Ok(message);
    }
}