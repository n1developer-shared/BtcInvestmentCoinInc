using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BtcInvestmentInc.Shared.Models.Investment
{
    public class InvestmentDetail
    {

        public double Payment { get; set; }
        public double Profit { get; set; }
        public DateTime Date { get; set; }
        public int PlanId { get; set; }
    }
}
