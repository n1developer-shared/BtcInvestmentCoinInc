using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BtcInvestmentInc.Shared.Models.User
{
    public class RegistrationModel
    {
        [Required]
        [MinLength(6,ErrorMessage = "Username should be at least 6 character long")]
        [MaxLength(12,ErrorMessage = "Username can't exceed 12 length")]
        public string Username { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password to short, minimum 6 character long")]
        [MaxLength(20,ErrorMessage = "Password exceed 20 characters!")]
        public string Password { get; set; }
        
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password not matched!")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
