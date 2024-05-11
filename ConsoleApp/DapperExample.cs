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
}