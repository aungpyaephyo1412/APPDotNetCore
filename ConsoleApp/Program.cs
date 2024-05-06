// See https://aka.ms/new-console-template for more information

using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder(); // Default value null
stringBuilder.DataSource = "."; /* Server Name */
stringBuilder.InitialCatalog = "DotNetSLH";
stringBuilder.UserID = "sa";
stringBuilder.Password = "Typle@14122003";

SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);

connection.Open();
Console.WriteLine("Connection Open.");

string query = "SELECT * FROM TblBlog";
SqlCommand cmd = new SqlCommand(query, connection);
SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
DataTable dataTable = new DataTable();
sqlDataAdapter.Fill(dataTable);
connection.Close();
Console.WriteLine("Connection Close.");

foreach (DataRow dataRow in dataTable.Rows)
{
    Console.WriteLine(dataRow["BlogId"]);
    Console.WriteLine(dataRow["BlogTitle"]);
    Console.WriteLine(dataRow["BlogAuthor"]);
    Console.WriteLine(dataRow["BlogContent"]);
}

Console.ReadKey();