using Microsoft.EntityFrameworkCore;

namespace ConsoleApp;

public class AppDbConnect : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        // base.OnConfiguring(optionsBuilder)
    }

    public DbSet<BlogDto> Blogs { get; set; }
}