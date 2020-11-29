using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BtcInvestmentInc.Server.Models;
using Microsoft.AspNetCore.Identity;

namespace BtcInvestmentInc.Server.Helper
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string BtcWalletAddress { get; set; }
    }
}
