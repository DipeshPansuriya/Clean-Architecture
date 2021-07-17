using Application_Domain;
using System.Threading.Tasks;

namespace Application_Core.Repositories
{
    public interface IRepositoryAsync<T> where T : class
    {
        //IQueryable<T> Entities { get; }

        Task<Response> SaveAsync(T entity, bool IsCache, string Cahekey);

        Task<Response> SaveAsync(T entity);

        Task<Response> DeleteAsync(T entity, bool IsCache, string Cahekey);

        Task<Response> DeleteAsync(T entity);

        Task<Response> UpdateAsync(T entity, bool IsCache, string Cahekey);

        Task<Response> UpdateAsync(T entity);

        Task<T> GetDetails(int id);
    }
}