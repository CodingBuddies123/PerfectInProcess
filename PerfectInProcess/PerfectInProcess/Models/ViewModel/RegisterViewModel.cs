using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerfectInProcess.Models.ViewModel
{
    public class RegisterViewModel : BaseViewModel
    {
        
        [Required(ErrorMessage = "User Name field is required")]
        [RegularExpression(@"^[A-Za-z0-9 ]+$", ErrorMessage = @"Username can not contain special characters.")]
        [MinLength(3, ErrorMessage = "User Name must be at least 4 Characters")]
        [MaxLength(20, ErrorMessage = "User Name must be less than 20 characters")]
        public string UserName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters and contain a special character and number")]
        [MaxLength(20, ErrorMessage = "Password must be less than 20 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*(_|[^\w])).+$", ErrorMessage = @"Password must have one capital, one special character and one numerical character. It can not start with a special character or a digit.")]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "First name field is required")]     
        public string FirstName { get; set; }     
        [Required(ErrorMessage = "Last name field is required")]
        public string LastName { get; set; }     
   
    }
}