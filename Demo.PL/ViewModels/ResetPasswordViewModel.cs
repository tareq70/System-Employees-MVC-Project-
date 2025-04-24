using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "New Password is Required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage ="Password Dosn't Match") ]
        public string ConfirmPassword { get; set; }
    }
}
