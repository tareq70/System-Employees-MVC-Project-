using BLL.Interfaces;
using DAL.Dbcontext;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MvcAppDbContext _dbContext;

        public EmployeeRepository(MvcAppDbContext dbContext):base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public IQueryable<Employee> GetEmployeeByName(string SearchedValue)
        {
            return _dbContext.Employees.Where(E => E.Name.ToLower().Contains(SearchedValue));
        }

        IQueryable<Employee> IEmployeeRepository.GetEmployeeByAddress(string adress)
        => _dbContext.Employees.Where(E=>E.Address.Equals(adress));
        
    }
}
