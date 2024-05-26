namespace RestApiWithNLayer;

public static class ConnectionString
{
    public static readonly SqlConnectionStringBuilder SqlConnectionStringBuilder = new()
    {
        DataSource = ".", /* Server Name */
        InitialCatalog = "DotNetSLH",
        UserID = "sa",
        Password = "Typle@14122003",
        TrustServerCertificate = true
    };
}