using LeaveManagement.Domain.Common;

namespace LeaveManagement.Application.Interfaces.Persistence;

public interface IRepository<T> where T : BaseEntity
{
    Task<T> GetAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
}