using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Dbcontext
{

    public class MvcAppDbContext : IdentityDbContext <ApplicationUser>
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer();
        //}
        public MvcAppDbContext(DbContextOptions<MvcAppDbContext> options) : base(options)
        {

        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
