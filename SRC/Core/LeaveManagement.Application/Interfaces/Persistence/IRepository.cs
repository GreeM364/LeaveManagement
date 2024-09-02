using LeaveManagement.Domain.Common;

namespace LeaveManagement.Application.Interfaces.Persistence;

public interface IRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}