using System.Data;
using System.Data.SqlClient;

namespace ConsoleApp
{
    internal class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".", /* Server Name */
            InitialCatalog = "DotNetSLH",
            UserID = "sa",
            Password = "Typle@14122003",
        };

        public void Read()
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
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
        }

        public void Create(string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open.");

            string query = @"INSERT INTO TblBlog ([BlogTitle],[BlogAuthor],[BlogContent]) VALUES (@blogTitle,@blogAuthor,@blogContent)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@blogTitle", title);
            cmd.Parameters.AddWithValue("@blogAuthor", author);
            cmd.Parameters.AddWithValue("@blogContent", content);
            cmd.ExecuteNonQuery();

            connection.Close();
            Console.WriteLine("Connection Close.");
        }
    }
}