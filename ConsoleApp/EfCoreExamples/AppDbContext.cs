using ConsoleApp.Dtos;
using ConsoleApp.Services;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.EfCoreExamples;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        // base.OnConfiguring(optionsBuilder)
    }

    public DbSet<BlogDto> Blogs { get; set; }
}