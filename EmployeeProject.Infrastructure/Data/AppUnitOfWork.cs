using EmployeeProject.Core.Interfaces;
using EmployeeProject.Infrastructure.Data.Context;

namespace EmployeeProject.Infrastructure.Data
{
    internal class AppUnitOfWork : UnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext context) : base(context)
        {

        }
    }
}
