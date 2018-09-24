using ToDoApi.Models;

namespace ToDoApi.Data
{
    public interface IEntityRepository<TEntity>
        : IEntityRepository<TEntity, int>
        where TEntity : class, IIdentifiable<int>
    { }

    public interface IEntityRepository<TEntity, TId>
        : IEntityReadRepository<TEntity, TId>,
        IEntityWriteRepository<TEntity, TId>
        where TEntity : class, IIdentifiable<TId>
    { }

}
