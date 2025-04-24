using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is Required")]
        [MaxLength(50)]
        public String Name { get; set; }

        [Required]
        public String Code { get; set; }
        public DateTime CreationOfDate { get; set; }
        [InverseProperty("Department")]

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();


    }
}
