using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLC.Core.ViewModels
{
    public class SignInVM
    {
        [Required(ErrorMessage = "ThisField Required")]
        [EmailAddress(ErrorMessage = "Invalid Mail")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "ThisField Required")]
        [MinLength(6, ErrorMessage = "Min Length 6")]
        public string? Password { get; set; }

       
    }
}
