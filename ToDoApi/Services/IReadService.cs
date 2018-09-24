using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApi.Models;

namespace ToDoApi.Services
{

    public interface IReadService<T> : IReadService<T, int>
       where T : class, IIdentifiable<int>
    { }

    public interface IReadService<T, TId>
        where T : class, IIdentifiable<TId>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(TId id);
    }

}