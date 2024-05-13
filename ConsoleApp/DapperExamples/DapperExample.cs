using System.Data;
using System.Data.SqlClient;
using ConsoleApp.Dtos;
using ConsoleApp.Services;
using Dapper;

namespace ConsoleApp.DapperExamples;

public class DapperExample
{
    public void Run()
    {
        // Read();
    }

    private void Read()
    {
        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        List<BlogDto> list = db.Query<BlogDto>("select * from Tbl_Blog").ToList();
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
        string query = @"INSERT INTO Tbl_Blog ([BlogTitle],[BlogAuthor],[BlogContent]) VALUES (@blogTitle,@blogAuthor,@blogContent)";
        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        db.Execute(query,item);
    }
    
    private void Edit(int id)
    {
        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        var item = db.Query("select * from Tbl_Blog where id = @BlogId",new BlogDto{BlogId = id}).FirstOrDefault();
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
        string query = @"UPDATE Tbl_Blog SET [BlogTitle] = @BlogTitle,[BlogAuthor] = @BlogAuthor,[BlogContent]=@BlogContent WHERE BlogId=@BlogId";
        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        db.Execute(query,item);
    }
    
    private void Delete(int id)
    {
        var item = new BlogDto
        {
            BlogId = id
        };
        string query = @"DELETE FROM Tbl_Blog WHERE [BlogId]=@BlogId";

        using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        db.Execute(query,item);
    }

}