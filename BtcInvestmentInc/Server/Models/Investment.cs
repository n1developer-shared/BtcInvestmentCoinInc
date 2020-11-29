using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BtcInvestmentInc.Server.Helper;

namespace BtcInvestmentInc.Server.Models
{
    public class Investment
    {
        [Key]
        public int Id { get; set; }
        public double Payment { get; set; }
        public int Profit { get; set; }
        public DateTime Date { get; set; }
        public int PlanId { get; set; }
        public bool Confirmed { get; set; }
        public string UserId { get; set; }
    }
}
