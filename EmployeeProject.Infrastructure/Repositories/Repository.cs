using EmployeeProject.Core.Entities;
using EmployeeProject.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeProject.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext _dbContext;
        internal DbSet<T> DbSet;
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entity)
        {
            DbSet.AddRange(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entity)
        {
            await DbSet.AddRangeAsync(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return await DbSet.AnyAsync(filter);
        }

        public IQueryable<T> AsQueryable()
        {
            return DbSet;
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> filter)
        {
            return await DbSet.Where(filter).CountAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool tracked = true, bool ignoreQueryFilter = false)
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
                query = query.Where(filter);

            query = IncludeProperties(query, includeProperties);
            query = AsNoTracking(query, tracked);
            query = IgnoreQueryFilter(query, ignoreQueryFilter);

            return await query.ToListAsync();
        }


        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true, bool ignoreQueryFilter = false)
        {
            IQueryable<T> query = DbSet;
            query = query.Where(filter);
            query = IncludeProperties(query, includeProperties);
            query = AsNoTracking(query, tracked);
            query = IgnoreQueryFilter(query, ignoreQueryFilter);

            return await query.FirstOrDefaultAsync();
        }


        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entity)
        {
            DbSet.UpdateRange(entity);
        }

        private IQueryable<T> IncludeProperties(IQueryable<T> query, string? includeProperties)
        {
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(",", StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query;
        }

        private IQueryable<T> AsNoTracking(IQueryable<T> query, bool tracked)
        {
            if (tracked == false)
                return query.AsNoTracking();

            return query;
        }

        private IQueryable<T> IgnoreQueryFilter(IQueryable<T> query, bool ignoreQueryFilter)
        {
            if (ignoreQueryFilter == true)
                return query.IgnoreQueryFilters();

            return query;
        }
    }
}
