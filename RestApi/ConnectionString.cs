using System.Data.SqlClient;

namespace RestApi;

public static class ConnectionString
{
    public static readonly SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = ".", /* Server Name */
        InitialCatalog = "DotNetSLH",
        UserID = "sa",
        Password = "Typle@14122003",
        TrustServerCertificate = true
    };
}