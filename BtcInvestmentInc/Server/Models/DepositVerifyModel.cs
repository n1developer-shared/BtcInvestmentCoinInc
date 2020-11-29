using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BtcInvestmentInc.Server.Models
{
    public class DepositVerifyModel
    {
        public string UserId { get; set; }
        public string Invoice { get; set; }
    }
}
