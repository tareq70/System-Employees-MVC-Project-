using BLL.Interfaces;
using BLL.Repositories;
using DAL.Dbcontext;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class DepartmentRepository : GenericRepository <Department>, IDepartmentRepository
    {

        public DepartmentRepository(MvcAppDbContext dbContext):base(dbContext)
        {
        }

    }
}
