using Microsoft.EntityFrameworkCore;
using MyTodo_sqlite.Models;

namespace MyTodo_sqlite.Data;

public class AppDbContext : DbContext
{
    public DbSet<Todo> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");
}