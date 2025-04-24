using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Required]
        public string Fname { get; set; }
		[Required]
		public string Lname { get; set; }
		[Required]
		public bool IsAgree { get; set; }




	}
}
