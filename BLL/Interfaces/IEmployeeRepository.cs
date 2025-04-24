using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IEmployeeRepository: IGenericRepository<Employee>
    {
        IQueryable<Employee> GetEmployeeByAddress(string adress);
        IQueryable<Employee> GetEmployeeByName(string SearchedValue);
       
    }
}
