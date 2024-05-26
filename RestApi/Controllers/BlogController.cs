using Microsoft.AspNetCore.Mvc;
using RestApi.Database;
using RestApi.Models;

namespace RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly AppDbContext _db;

    public BlogController()
    {
        _db = new AppDbContext();
    }

    [HttpGet]
    public IActionResult Read()
    {
        var data = _db.Blogs.ToList();
        return Ok(data);
    }

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
        var data = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (data is null) return NotFound("No data found!");
        return Ok(data);
    }

    [HttpPost]
    public IActionResult Create(BlogModel blogModel)
    {
        _db.Blogs.Add(blogModel);
        var result = _db.SaveChanges();
        var message = result > 0 ? "Success" : "Fail";
        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, BlogModel blogModel)
    {
        var data = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (data is null) return NotFound("No data found!");

        data.BlogTitle = blogModel.BlogTitle;
        data.BlogAuthor = blogModel.BlogAuthor;
        data.BlogContent = blogModel.BlogContent;
        var result = _db.SaveChanges();
        var message = result > 0 ? "Success" : "Fail";
        return Ok(message);
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, BlogModel blogModel)
    {
        var data = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (data is null) return NotFound("No data found!");

        if (!string.IsNullOrEmpty(blogModel.BlogTitle)) data.BlogTitle = blogModel.BlogTitle;
        if (!string.IsNullOrEmpty(blogModel.BlogAuthor)) data.BlogAuthor = blogModel.BlogAuthor;
        if (!string.IsNullOrEmpty(blogModel.BlogContent)) data.BlogContent = blogModel.BlogContent;
        var result = _db.SaveChanges();
        var message = result > 0 ? "Success" : "Fail";
        return Ok(message);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null) return NotFound("Data not found!");

        _db.Blogs.Remove(item);
        _db.SaveChanges();
        return NoContent();
    }
}