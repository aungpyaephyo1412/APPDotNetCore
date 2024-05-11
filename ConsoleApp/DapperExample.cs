using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace ConsoleApp;

public class DapperExample
{
    public void Run()
    {
        // Read();
    }

    private void Read()
    {
        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        List<BlogDto> list = db.Query<BlogDto>("select * from tblBlog").ToList();
        foreach (var li in list)
        {
            Console.WriteLine(li.BlogId);
            Console.WriteLine(li.BlogTitle);
            Console.WriteLine(li.BlogAuthor);
            Console.WriteLine(li.BlogContent);
        }
    }
    
    private void Create(string title, string author, string content)
    {
        var item = new BlogDto
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };
        string query = @"INSERT INTO TblBlog ([BlogTitle],[BlogAuthor],[BlogContent]) VALUES (@blogTitle,@blogAuthor,@blogContent)";
        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        db.Execute(query,item);
    }
    
    private void Edit(int id)
    {
        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        var item = db.Query("select * from tblBlog where id = @BlogId",new BlogDto{BlogId = id}).FirstOrDefault();
        if (item == null)
        {
            Console.WriteLine("Data not found");
            return;
        }
    }
    
    private void Update(int id, string title, string author, string content)
    {
        var item = new BlogDto
        {
            BlogId = id,
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };
        string query = @"UPDATE TblBlog SET [BlogTitle] = @BlogTitle,[BlogAuthor] = @BlogAuthor,[BlogContent]=@BlogContent WHERE BlogId=@BlogId";
        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        db.Execute(query,item);
    }
    
    private void Delete(int id)
    {
        var item = new BlogDto
        {
            BlogId = id
        };
        string query = @"DELETE FROM TblBlog WHERE [BlogId]=@BlogId";

        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        db.Execute(query,item);
    }

}