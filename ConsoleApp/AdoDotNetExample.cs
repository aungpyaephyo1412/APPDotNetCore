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

            string query =
                @"INSERT INTO TblBlog ([BlogTitle],[BlogAuthor],[BlogContent]) VALUES (@blogTitle,@blogAuthor,@blogContent)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@blogTitle", title);
            cmd.Parameters.AddWithValue("@blogAuthor", author);
            cmd.Parameters.AddWithValue("@blogContent", content);
            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Insert success" : "Insert Fail";

            connection.Close();
            Console.WriteLine("Connection Close.");
            Console.WriteLine(message);
        }

        public void Edit(int id)
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
            if (dataTable.Rows.Count == 0)
            {
                Console.WriteLine("Empty data");
                return;
            }

            DataRow dataRow = dataTable.Rows[0];
            Console.WriteLine(dataRow["BlogId"]);
            Console.WriteLine(dataRow["BlogTitle"]);
            Console.WriteLine(dataRow["BlogAuthor"]);
            Console.WriteLine(dataRow["BlogContent"]);
        }

        public void Update(int id, string title, string author, string content)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open.");

            string query =
                @"UPDATE TblBlog SET [BlogTitle] = @BlogTitle,[BlogAuthor] = @BlogAuthor,[BlogContent]=@BlogContent WHERE BlogId=@BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Update success" : "Update Fail";

            connection.Close();
            Console.WriteLine("Connection Close.");
            Console.WriteLine(message);
        }

        public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open.");

            string query = @"DELETE FROM TblBlog WHERE [BlogId]=@BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Delete success" : "Delete Fail";

            connection.Close();
            Console.WriteLine("Connection Close.");
            Console.WriteLine(message);
        }
    }
}