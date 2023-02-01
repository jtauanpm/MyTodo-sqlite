using Microsoft.EntityFrameworkCore;

namespace MyTodo_sqlite.Data;

public class AppDbContextFactory : IAppDbContextFactory
{
    public AppDbContext DbContextConstructed()
    {
        var db = new AppDbContext();
        db.Database.EnsureCreated();
        return db;
    }
}