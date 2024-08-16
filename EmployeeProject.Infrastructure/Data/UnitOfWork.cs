using EmployeeProject.Core.Entities;
using EmployeeProject.Core.Entities.User;
using EmployeeProject.Core.Interfaces;
using EmployeeProject.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;

namespace EmployeeProject.Infrastructure.Data
{
    internal class UnitOfWork<T> : IUnitOfWork where T : DbContext
    {

        private readonly T _dbContext;
        private Hashtable _repositories;

        public UnitOfWork(T _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }


        public async Task<bool> ExecuteProcedureAsync(string command, IEnumerable<SqlParameter>? parameters = null)
        {
            if (parameters == null)
                return await _dbContext.Database.ExecuteSqlRawAsync(command) > 0;

            return await _dbContext.Database.ExecuteSqlRawAsync(command, parameters.ToArray()) > 0;
        }


        public async Task<IEnumerable<TResult>> ExecuteProcedureAsync<TResult>(string command, IEnumerable<SqlParameter>? parameters = null)
        {
            if (parameters == null)
                return await _dbContext.Database.SqlQueryRaw<TResult>(command).ToListAsync();

            return await _dbContext.Database.SqlQueryRaw<TResult>(command, parameters.ToArray()).ToListAsync();
        }

        public IRepository<TEntity> repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null) _repositories = new Hashtable();
            var Type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(Type))
            {
                var repositiryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(
                    repositiryType.MakeGenericType(typeof(TEntity)), _dbContext);
                _repositories.Add(Type, repositoryInstance);
            }
            return (IRepository<TEntity>)_repositories[Type];
        }
    }
}
