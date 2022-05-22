using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.SeedWork;
using OrderService.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly OrderDBContext dBContext;
        public IUnitOfWork UnitOfWork => dBContext;



        public GenericRepository(OrderDBContext dBContext)
        {
            this.dBContext = dBContext;
        }





        public virtual async Task<T> AddAsync(T entity)
        {
            await dBContext.Set<T>().AddAsync(entity);
            return entity;
        }
        public virtual T Update(T entity)
        {
            dBContext.Set<T>().Update(entity);
            return entity;
        }




        public virtual Task<List<T>> Get(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            return Get(filter, null, includes);
        }
        public async Task<List<T>> Get(Expression<Func<T, bool>> filter = null,
                                 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                 params Expression<Func<T, object>>[] includes)
        {

            IQueryable<T> query = dBContext.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }




        public virtual async Task<List<T>> GetAll()
        {
            return await dBContext.Set<T>().ToListAsync();
        }




        public virtual async Task<T> GetById(Guid id)
        {
            return await dBContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dBContext.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }





        public virtual async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dBContext.Set<T>();

            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            return await query.SingleOrDefaultAsync();
        }


    }
}
