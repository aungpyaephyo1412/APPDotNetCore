using RestApiWithNLayer.Database;

namespace RestApiWithNLayer.Features.Blog;

public class DA_Blog
{
    private readonly AppDbContext _context;

    public DA_Blog()
    {
        _context = new AppDbContext();
    }

    public List<BlogModel> GetBlogs()
    {
        var lst = _context.Blogs.ToList();
        return lst;
    }

    public BlogModel GetBlog(int id)
    {
        var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
        return item;
    }

    public int CreateBlog(BlogModel reqModel)
    {
        _context.Blogs.Add(reqModel);
        var result = _context.SaveChanges();
        return result;
    }

    public int UpdateBlog(int id, BlogModel reqModel)
    {
        var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null) return 0;

        item.BlogTitle = reqModel.BlogTitle;
        item.BlogAuthor = reqModel.BlogAuthor;
        item.BlogContent = reqModel.BlogContent;
        var result = _context.SaveChanges();
        return result;
    }

    public int DeleteBlog(int id)
    {
        var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null) return 0;
        _context.Remove(item);
        var result = _context.SaveChanges();
        return result;
    }
}