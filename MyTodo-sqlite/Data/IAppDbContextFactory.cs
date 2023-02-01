using Microsoft.EntityFrameworkCore;

namespace MyTodo_sqlite.Data;

public interface IAppDbContextFactory
{
    public AppDbContext DbContextConstructed();
}