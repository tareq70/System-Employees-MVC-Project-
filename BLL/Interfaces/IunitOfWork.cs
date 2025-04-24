using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IunitOfWork
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }

         Task<int> CompleteAsync();
        //void Dispose();
    }
}
