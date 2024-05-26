namespace RestApiWithNLayer.Database;

public class AppDbContext : DbContext
{
    public DbSet<BlogModel> Blogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        // base.OnConfiguring(optionsBuilder)
    }
}