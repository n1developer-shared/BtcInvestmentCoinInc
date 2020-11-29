using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BtcInvestmentInc.Shared.Models.User
{
    public class AuthenticationModel
    {
        [Required]
        [MinLength(6)]
        [MaxLength(12)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string Password { get; set; }
    }
}
