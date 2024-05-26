using Microsoft.AspNetCore.Mvc;

namespace RestApiWithNLayer.Features.Blog;
[Route("api/[controller]")]
[ApiController]
public class BlogController1 : ControllerBase
{
   private readonly BL_Blog _blBlog;

    public BlogController1()
    {
        _blBlog = new BL_Blog();
    }

    [HttpGet]
    public IActionResult Read()
    {
        var data = _blBlog.GetBlogs();
        return Ok(data);
    }

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
        var data = _blBlog.GetBlog(id);
        return Ok(data);
    }

    [HttpPost]
    public IActionResult Create(BlogModel blogModel)
    {
        var result = _blBlog.CreateBlog(blogModel);
        var message = result > 0 ? "Success" : "Fail";
        return Ok(message);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, BlogModel blogModel)
    {
       var result = _blBlog.UpdateBlog(id,blogModel);
        var message = result > 0 ? "Success" : "Fail";
        return Ok(message);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _blBlog.DeleteBlog(id);
        return NoContent();
    }
}