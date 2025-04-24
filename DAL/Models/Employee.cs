using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string ImageName { get; set; }   
        public int? Age { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [InverseProperty("Employees")]
        public Department Department { get; set; }
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
    }
}
