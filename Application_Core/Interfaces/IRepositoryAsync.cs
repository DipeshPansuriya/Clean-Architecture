using System.Threading.Tasks;

namespace Application_Core.Interfaces
{
    public interface IRepositoryAsync<T> where T : class
    {
        //IQueryable<T> Entities { get; }

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}