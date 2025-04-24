using BLL.Interfaces;
using BLL.Repository;
using DAL.Dbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class UnitOfWork : IunitOfWork, IDisposable
    {
        private readonly MvcAppDbContext _dbContext;

        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }

        public UnitOfWork(MvcAppDbContext dbContext)
        {
            EmployeeRepository = new EmployeeRepository(dbContext);
            DepartmentRepository = new DepartmentRepository(dbContext);
            _dbContext = dbContext;

        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
             _dbContext?.Dispose();
        }
    }
}
