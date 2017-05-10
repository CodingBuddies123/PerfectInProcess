using System.ComponentModel.DataAnnotations;

namespace PerfectInProcess.Models.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Username/Email cannot be blank.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password cannot be blank.")]
        public string Password { get; set; }
    }
}