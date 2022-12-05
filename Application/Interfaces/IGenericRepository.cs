using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IGenericRepository<T>
{
    public Task<T> CreateAsync(T entity);
    public Task<IEnumerable<T>> ListAll();
    public Task<IEnumerable<T>> FilterMany(Expression<Func<T, bool>> query);
    public Task<T> FilterOne(Expression<Func<T, bool>> query);
    public Task<T> GetById(string id);
    public Task<T> UpdateAsync(T entity);
    public Task<bool> DeleteAsync(T entity);
    public Task<int> Complete();
}