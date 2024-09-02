using LeaveManagement.Application.Interfaces.Persistence;
using LeaveManagement.Domain.Common;
using LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly LeaveManagementDatabaseContext _context;

    public Repository(LeaveManagementDatabaseContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<T>> GetAsync()
    {
        var entities = await _context
            .Set<T>()
            .AsNoTracking()
            .ToListAsync();
        
        return entities;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        var entity = await _context
            .Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        
        return entity;
    }
    
    public async Task CreateAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }
}