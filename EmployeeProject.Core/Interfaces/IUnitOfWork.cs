using EmployeeProject.Core.Entities;
using Microsoft.Data.SqlClient;

namespace EmployeeProject.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> Complete();
        void Dispose();
        Task<bool> ExecuteProcedureAsync(string command, IEnumerable<SqlParameter>? parameters = null);
        Task<IEnumerable<TResult>> ExecuteProcedureAsync<TResult>(string command, IEnumerable<SqlParameter>? parameters = null);
        IRepository<TEntity> repository<TEntity>() where TEntity : BaseEntity;
    }
}
