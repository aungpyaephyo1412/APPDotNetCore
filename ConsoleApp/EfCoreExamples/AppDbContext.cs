using ConsoleApp.Dtos;
using ConsoleApp.Services;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.EfCoreExamples;

public class AppDbContext : DbContext
{
    public DbSet<BlogDto> Blogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        // base.OnConfiguring(optionsBuilder)
    }
}