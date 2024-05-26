using Microsoft.AspNetCore.Mvc;
using RestApi.Models;
using Shared;

namespace RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogDapper2Controller : ControllerBase
{
    private readonly DapperService _dapperService = new(ConnectionString.SqlConnectionStringBuilder.ConnectionString);

    [HttpGet]
    public IActionResult GetBlogs()
    {
        var query = "select * from Tbl_Blog";
        var list = _dapperService.Query<BlogModel>(query);
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
        _dapperService.Execute(query, blog);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBlog(int id, BlogModel blog)
    {
        var query =
            @"UPDATE Tbl_Blog SET [BlogTitle] = @BlogTitle,[BlogAuthor] = @BlogAuthor,[BlogContent]=@BlogContent WHERE BlogId=@BlogId";
        var item = FindById(id);
        if (item is null) return NotFound();

        blog.BlogId = id;
        _dapperService.Execute(query, blog);
        return Ok();
    }

    [HttpPatch("{id}")]
    public IActionResult PathBlog(int id, BlogModel blog)
    {
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
        _dapperService.Execute(query, blog);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBlog(int id)
    {
        var item = FindById(id);
        if (item is null) return NotFound();

        var query = @"DELETE FROM Tbl_Blog WHERE [BlogId]=@BlogId";

        _dapperService.Execute(query, new BlogModel { BlogId = id });
        return NoContent();
    }

    private BlogModel? FindById(int id)
    {
        var query = "select * from Tbl_Blog where id = @BlogId";
        return _dapperService.QueryFirstOrDefault<BlogModel>(query, new BlogModel { BlogId = id });
    }
}