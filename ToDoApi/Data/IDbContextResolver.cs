using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Data
{
    public interface IDbContextResolver
    {
        DbContext GetContext();
        DbSet<TEntity> GetDbSet<TEntity>() 
            where TEntity : class;
    }
}
