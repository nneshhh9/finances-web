using Application.Models;
using Microsoft.EntityFrameworkCore;
using Type = System.Type;

namespace Persistence.Data;

/// <summary>
/// Контекст БД для PostgreSQL
/// </summary>
public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) 
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Type> Types { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(i => i.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .Property(u => u.Balance)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Goal>()
            .Property(u => u.GoalBalance)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Transaction>()
            .Property(u => u.Balance)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Transaction>()
            .Property(u => u.Change)
            .HasColumnType("decimal(18,2)");
    }
}