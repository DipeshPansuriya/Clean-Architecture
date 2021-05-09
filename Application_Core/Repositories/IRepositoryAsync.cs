using Application_Domain;
using System.Threading.Tasks;

namespace Application_Core.Repositories
{
    public interface IRepositoryAsync<T> where T : class
    {
        //IQueryable<T> Entities { get; }

        Task<Response> AddAsync(T entity);

        Task<Response> DeleteAsync(T entity);

        Task<Response> UpdateAsync(T entity);

        Task<T> GetDetails(int id);
    }
}