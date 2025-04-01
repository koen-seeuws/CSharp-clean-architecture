using Microsoft.EntityFrameworkCore;
using MyTodos.Core.Domain;

namespace MyTodos.Infrastructure.DataAccess.Configuration;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Todo> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>(library =>
        {
            library.HasKey(l => l.Id);

            library.Property(l => l.Title).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}