
using Microsoft.EntityFrameworkCore;
using RestApi.Models;

namespace RestApi.Database;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        // base.OnConfiguring(optionsBuilder)
    }

    public DbSet<BlogModel> Blogs { get; set; }
}