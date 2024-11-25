using Entities;
using Microsoft.EntityFrameworkCore;

namespace EfcRepositories;
public class AppDbContext : DbContext
{
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        optionsBuilder.UseSqlite(@"Data Source=C:\Users\adres\RiderProjects\Assignment 1.net\Server\EfcRepositories\app.db");
    }
    
  
}