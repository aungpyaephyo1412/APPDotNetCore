using Refit;

namespace ConsoleAppRefitExample;

public interface IBlogApi
{
    [Get("/api/blog")]
    Task<List<BlogModel>> GetBlogs();
    
    [Get("/api/blog/{id}")]
    Task<BlogModel> GetBlog(int id);
}

public abstract class BlogModel
{
    public string? BlogTitle { get; set; }
    public string? BlogAuthor { get; set; }
    public string? BlogContent { get; set; }
    // public record Dto (int BlogId,string BlogTitle,string BlogAuthor,string BlogContent)
}