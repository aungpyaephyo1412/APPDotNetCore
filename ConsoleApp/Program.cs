// See https://aka.ms/new-console-template for more information

using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
stringBuilder.DataSource = ".";
stringBuilder.InitialCatalog = "DotNetSLH";
stringBuilder.UserID = "sa";
stringBuilder.Password = "Typle@14122003";

SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);

connection.Open();
Console.WriteLine("Connection Open.");

connection.Close();
Console.WriteLine("Connection Close.");

Console.ReadKey();