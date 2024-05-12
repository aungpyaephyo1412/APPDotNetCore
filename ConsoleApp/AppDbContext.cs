using Microsoft.EntityFrameworkCore;

namespace ConsoleApp;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        // base.OnConfiguring(optionsBuilder)
    }

    public DbSet<BlogDto> Blogs { get; set; }
}