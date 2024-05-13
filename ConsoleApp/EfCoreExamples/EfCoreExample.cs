using ConsoleApp.Dtos;

namespace ConsoleApp.EfCoreExamples;

public class EfCoreExample
{
    private readonly AppDbContext _db = new AppDbContext();
    public void Run()
    {
        // Read();
        // Edit(3);
    }

    private void Read()
    {
        var items = _db.Blogs.ToList();
        foreach (var li in items)
        {
            Console.WriteLine(li.BlogId);
            Console.WriteLine(li.BlogTitle);
            Console.WriteLine(li.BlogAuthor);
            Console.WriteLine(li.BlogContent);
        }
    }

    private void Edit(int id)
    {
        var item = _db.Blogs.FirstOrDefault(x=>x.BlogId == id);
        if (item is null)
        {
            Console.WriteLine("Data not found!");
            return;
        }
        Console.WriteLine(item.BlogId);
        Console.WriteLine(item.BlogTitle);
        Console.WriteLine(item.BlogAuthor);
        Console.WriteLine(item.BlogContent);
    }

    private void Create(string title, string author, string content)
    {
        var item = new BlogDto
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };
        _db.Blogs.Add(item);
        int result = _db.SaveChanges();
        string message = result > 0 ? "Success" : "Fail";
        Console.WriteLine(message);
    }

    private void Update(int id, string title, string author, string content)
    {
        var item = _db.Blogs.FirstOrDefault(x=>x.BlogId == id);
        if (item is null)
        {
            Console.WriteLine("Data not found!");
            return;
        }
        item.BlogTitle = title;
        item.BlogAuthor = author;
        item.BlogContent = content;
        int result = _db.SaveChanges();
        string message = result > 0 ? "Success" : "Fail";
        Console.WriteLine(message);
    }

    private void Delete(int id)
    {
        var item = _db.Blogs.FirstOrDefault(x=>x.BlogId == id);
        if (item is null)
        {
            Console.WriteLine("Data not found!");
            return;
        }

        _db.Blogs.Remove(item);
        int result = _db.SaveChanges();
        string message = result > 0 ? "Success" : "Fail";
        Console.WriteLine(message);
    }
}