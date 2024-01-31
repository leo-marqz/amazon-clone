
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ecommerce.Application.Abstracts;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RepositoryBase<T> : IGenericRepository<T> where T : class
    {
        private readonly EcommerceDBContext _context;
        public RepositoryBase(EcommerceDBContext context)
        {
            this._context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public void AddEntity(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void AddRange(List<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public void DeleteEntity(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IReadOnlyList<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeString, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if(disableTracking){
                query = query.AsNoTracking();
            }
            if(string.IsNullOrEmpty(includeString)){
                query = query.Include(includeString);
            }
            if(predicate != null){
                query = query.Where(predicate);
            }
            if(orderBy != null){
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();

        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if(disableTracking){
                query = query.AsNoTracking();
            }
            if(includes != null){
                query = includes.Aggregate(query, (current, include)=>current.Include(include));
            }
            if(predicate != null){
                query = query.Where(predicate);
            }
            if(orderBy != null){
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
              
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return (await _context.Set<T>().FindAsync(id))!;
        }

        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if(disableTracking){
                query = query.AsNoTracking();
            }
            if(includes != null){
                query = includes.Aggregate(query, (current, include)=>current.Include(include));
            }
            if(predicate != null){
                query = query.Where(predicate);
            }

            return (await query.FirstOrDefaultAsync())!;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public void UpdateEntity(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}