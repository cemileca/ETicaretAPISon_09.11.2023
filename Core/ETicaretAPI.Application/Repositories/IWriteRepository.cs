using ETicaretAPI.Domain.Entities.Common;

namespace ETicaretAPI.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> datas);
        bool Remove(T entity);
        bool RemoveRange(List<T> entity);
        Task<bool> RemoveById(string id);
        bool UpdateAsync(T entity);
        Task<int> SaveAsync();

    }
}
