using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RestApi.Models;

namespace RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogDapperController : ControllerBase
{
    [HttpGet]
    public IActionResult GetBlogs()
    {
        var query = "select * from Tbl_Blog";
        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        var list = db.Query<BlogModel>(query).ToList();
        return Ok(list);
    }

    [HttpGet("{id}")]
    public IActionResult GetBlog(int id)
    {
        var item = FindById(id);
        if (item is null) return NotFound();

        return Ok();
    }

    [HttpPost]
    public IActionResult CreateBlog(BlogModel blog)
    {
        var query =
            @"INSERT INTO Tbl_Blog ([BlogTitle],[BlogAuthor],[BlogContent]) VALUES (@blogTitle,@blogAuthor,@blogContent)";
        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        db.Execute(query, blog);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBlog(int id, BlogModel blog)
    {
        var query =
            @"UPDATE Tbl_Blog SET [BlogTitle] = @BlogTitle,[BlogAuthor] = @BlogAuthor,[BlogContent]=@BlogContent WHERE BlogId=@BlogId";
        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        var item = FindById(id);
        if (item is null) return NotFound();

        blog.BlogId = id;
        db.Execute(query, blog);
        return Ok();
    }

    [HttpPatch("{id}")]
    public IActionResult PathBlog(int id, BlogModel blog)
    {
        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        var item = FindById(id);
        if (item is null) return NotFound();

        var conditions = string.Empty;
        if (!string.IsNullOrEmpty(blog.BlogTitle)) conditions += "[BlogTitle] = @BlogTitle,";

        if (!string.IsNullOrEmpty(blog.BlogAuthor)) conditions += "[BlogAuthor] = @BlogAuthor,";

        if (!string.IsNullOrEmpty(blog.BlogContent)) conditions += "[BlogContent] = @BlogContent,";

        if (conditions.Length == 0) return NotFound();

        conditions = conditions.Substring(conditions.Length - 2);
        var query =
            $@"UPDATE Tbl_Blog SET {conditions}";
        blog.BlogId = id;
        db.Execute(query, blog);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBlog(int id)
    {
        var item = FindById(id);
        if (item is null) return NotFound();

        var query = @"DELETE FROM Tbl_Blog WHERE [BlogId]=@BlogId";

        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        db.Execute(query, new BlogModel { BlogId = id });
        return NoContent();
    }

    private BlogModel? FindById(int id)
    {
        var query = "select * from Tbl_Blog where id = @BlogId";
        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        return db.Query(query, new BlogModel { BlogId = id }).FirstOrDefault();
    }
}