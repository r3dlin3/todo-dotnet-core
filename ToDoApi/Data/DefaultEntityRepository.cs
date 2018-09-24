using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApi.Extensions;
using ToDoApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace ToDoApi.Data
{
    /// <inheritdoc />
    public class DefaultEntityRepository<TEntity>
        : DefaultEntityRepository<TEntity, int>,
        IEntityRepository<TEntity>
        where TEntity : class, IIdentifiable<int>
    {
        public DefaultEntityRepository(
            ILoggerFactory loggerFactory,
            IDbContextResolver contextResolver)
        : base(loggerFactory, contextResolver)
        { }
    }

    /// <summary>
    /// Provides a default repository implementation and is responsible for
    /// abstracting any EF Core APIs away from the service layer.
    /// </summary>
    public class DefaultEntityRepository<TEntity, TId>
        : IEntityRepository<TEntity, TId>
        where TEntity : class, IIdentifiable<TId>
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly ILogger _logger;


        public DefaultEntityRepository(
            ILoggerFactory loggerFactory,
            IDbContextResolver contextResolver)
        {
            _context = contextResolver.GetContext();
            _dbSet = contextResolver.GetDbSet<TEntity>();
            _logger = loggerFactory.CreateLogger<DefaultEntityRepository<TEntity, TId>>();
        }

        /// <inheritdoc />
        public virtual IQueryable<TEntity> Get()
        {

            return _dbSet;
        }

        /// <inheritdoc />
        public virtual IQueryable<TEntity> Filter(IQueryable<TEntity> entities, Expression<Func<TEntity,bool>> filterQuery)
        {
            return entities.Where(filterQuery);
        }

        /// <inheritdoc />
        /*public virtual IQueryable<TEntity> Sort(IQueryable<TEntity> entities, List<SortQuery> sortQueries)
        {
            return entities.Sort(sortQueries);
        }*/

        /// <inheritdoc />
        public virtual async Task<TEntity> GetAsync(TId id)
        {
            return await Get().SingleOrDefaultAsync(e => e.Id.Equals(id));
        }

        

        /// <inheritdoc />
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            _dbSet.Add(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        /// <summary>
        /// This is used to allow creation of HasMany relationships when the
        /// dependent side of the relationship already exists.
        /// </summary>

        
        /// <inheritdoc />
        public virtual async Task<TEntity> UpdateAsync(TId id, TEntity entity)
        {
            var oldEntity = await GetAsync(id);

            if (oldEntity == null)
                return null;

            await _context.SaveChangesAsync();

            return oldEntity;
        }


        /// <inheritdoc />
        public virtual async Task<bool> DeleteAsync(TId id)
        {
            var entity = await GetAsync(id);

            if (entity == null)
                return false;

            _dbSet.Remove(entity);

            await _context.SaveChangesAsync();

            return true;
        }

        /// <inheritdoc />
        

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TEntity>> PageAsync(IQueryable<TEntity> entities, int pageSize, int pageNumber)
        {
            if (pageNumber >= 0)
            {
                return await entities.PageForward(pageSize, pageNumber).ToListAsync();
            }

            // since EntityFramework does not support IQueryable.Reverse(), we need to know the number of queried entities
            int numberOfEntities = await this.CountAsync(entities);

            // may be negative
            int virtualFirstIndex = numberOfEntities - pageSize * Math.Abs(pageNumber);
            int numberOfElementsInPage = Math.Min(pageSize, virtualFirstIndex + pageSize);

            return await entities
                    .Skip(virtualFirstIndex)
                    .Take(numberOfElementsInPage)
                    .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<int> CountAsync(IQueryable<TEntity> entities)
        {
            return (entities is IAsyncEnumerable<TEntity>)
                 ? await entities.CountAsync()
                 : entities.Count();
        }

        /// <inheritdoc />
        public async Task<TEntity> FirstOrDefaultAsync(IQueryable<TEntity> entities)
        {
            return (entities is IAsyncEnumerable<TEntity>)
               ? await entities.FirstOrDefaultAsync()
               : entities.FirstOrDefault();
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<TEntity>> ToListAsync(IQueryable<TEntity> entities)
        {
            return (entities is IAsyncEnumerable<TEntity>)
                ? await entities.ToListAsync()
                : entities.ToList();
        }
    }
}
