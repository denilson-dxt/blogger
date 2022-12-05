using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T:BaseEntity
{
    private readonly DataContext _context;

    public GenericRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        return true;
    }

    public Task<IEnumerable<T>> FilterMany(System.Linq.Expressions.Expression<Func<T, bool>> query)
    {
        throw new NotImplementedException();
    }

    public Task<T> FilterOne(System.Linq.Expressions.Expression<Func<T, bool>> query)
    {
        throw new NotImplementedException();
    }

    public async Task<T> GetById(string id)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<T>> ListAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        return entity;
    }
}
