using Application_Genric;
using System.Threading.Tasks;

namespace Application_Core.Repositories
{
    public interface IRepositoryAsync<T> where T : class
    {
        //IQueryable<T> Entities { get; }

        Task<int> SaveAsync(T entity, bool IsCache, string Cahekey);

        Task<int> SaveAsync(T entity);

        Task<int> SaveNotificationAsync(NotficationCls entity);

        Task DeleteAsync(T entity, bool IsCache, string Cahekey);

        Task<bool> DeleteAsync(T entity);

        Task<int> UpdateAsync(T entity, bool IsCache, string Cahekey);

        Task<int> UpdateAsync(T entity);

        Task<int> UpdateNotificationAsync(NotficationCls entity);

        Task<T> GetDetails(int id);
    }
}