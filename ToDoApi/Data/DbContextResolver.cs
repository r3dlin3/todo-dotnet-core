using ToDoApi.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Data
{
    public class DbContextResolver<TContext> : IDbContextResolver
        where TContext : DbContext
    {
        private readonly TContext _context;

        public DbContextResolver(TContext context)
        {
            _context = context;
        }

        public DbContext GetContext() => _context;

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
            => _context.Set<TEntity>();
    }
}
