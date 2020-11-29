using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BtcInvestmentInc.Shared.Models.User
{
    public class Profile
    {
        [Required]
        [MaxLength(100)]
        [MinLength(10)]
        public string BtcWalletAddress { get; set; }
    }
}
