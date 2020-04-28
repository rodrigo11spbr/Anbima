using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AnbimaConsumer.Application.Infrastructure.Interfaces
{
    public interface IEntityRepository<T> where T : class, new()
    {
        Task Add(IEnumerable<T> objs);
        Task Add(T obj);
        Task<T> GetOne(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAll(int totalPerPage, int pageNumber);
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression, int totalPerPage, int pageNumber);
        Task Update(T obj);
        Task Remove(T obj);
    }
}