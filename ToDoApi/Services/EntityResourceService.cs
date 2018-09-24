using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Models;
using ToDoApi.Repository;

namespace ToDoApi.Services
{
    public class EntityResourceService<TResource, TEntity, TId> :
        IReadService<TResource, TId>
        where TResource : class, IIdentifiable<TId>
        where TEntity : class, IIdentifiable<TId>
    {
        // Create a field to store the mapper object
        private readonly IMapper _mapper;
        private readonly IEntityRepository<TEntity,TId> _repository;

        public EntityResourceService(IEntityRepository<TEntity,TId> repository,
                                        IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TResource>> GetAllAsync()
        {
            var entities = _repository.Get();
            return await entities.ProjectTo<TResource>().ToListAsync();
        }

        public async Task<TResource> GetByIdAsync(TId id)
        {
            var entity = await _repository.GetAsync(id);
            return _mapper.Map<TEntity,TResource>(entity);
        }
    }

}