using Microsoft.EntityFrameworkCore;
using TechLibrary.Domain.Entities;

namespace TechLibrary.Infraestructure.DataAccess;

public class TechLibraryDbContext : DbContext
{ 
    public DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SqlitePath");
        optionsBuilder.UseSqlite(connectionString);
    }
}