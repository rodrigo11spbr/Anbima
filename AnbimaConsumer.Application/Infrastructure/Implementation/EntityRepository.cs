using AnbimaConsumer.Application.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AnbimaConsumer.Application.Infrastructure.Implementation
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class, new()
    {
        public EntityRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        readonly IUnitOfWork UnitOfWork;

        public async Task Add(IEnumerable<T> objs)
        {
            await UnitOfWork.Context.Set<T>().AddRangeAsync(objs);
        }

        public async Task Add(T obj)
        {
            await UnitOfWork.Context.Set<T>().AddAsync(obj);
        }

        public async Task<IEnumerable<T>> GetAll(int totalPerPage, int pageNumber)
        {
            return await UnitOfWork.Context.Set<T>()
                .Skip(totalPerPage * pageNumber)
                .Take(pageNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression, int totalPerPage, int pageNumber)
        {
            return await UnitOfWork.Context.Set<T>()
               .Where(expression)
               .Skip(totalPerPage * pageNumber)
               .Take(pageNumber)
               .ToListAsync();
        }

        public async Task Update(T obj)
        {
            if (await UnitOfWork.Context.Set<T>().FindAsync(obj) != null)
            {
                UnitOfWork.Context.Entry(obj).State = EntityState.Modified;
                UnitOfWork.Context.Attach(obj);
            }
        }

        public async Task Remove(T obj)
        {
            if (await UnitOfWork.Context.Set<T>().FindAsync(obj) != null)
                UnitOfWork.Context.Set<T>().Remove(obj);
        }

        public async Task<T> GetOne(Expression<Func<T, bool>> expression)
        {
            return await UnitOfWork.Context.Set<T>().FirstOrDefaultAsync(expression);
            throw new NotImplementedException();
        }
    }
}
