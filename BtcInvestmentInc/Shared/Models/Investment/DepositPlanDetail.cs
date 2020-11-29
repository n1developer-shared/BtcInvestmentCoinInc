using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BtcInvestmentInc.Shared.Models.Investment
{
    public class DepositPlanDetail
    {
        public int Id { get; set; }
        public int Profit { get; set; }
        public int Duration { get; set; }   
        public int Min { get; set; }
        public int Max { get; set; }
        public bool Automatic { get; set; }

        public static List<DepositPlanDetail> CurrentPlans => new List<DepositPlanDetail>
        {
            new DepositPlanDetail()
            {
                Id = 1,
                Profit = 2,
                Duration = 10,
                Min = 100,
                Max = 1000,
                Automatic = true
            },
            new DepositPlanDetail()
            {
                Id = 2,
                Profit = 3,
                Duration = 15,
                Min = 1000,
                Max = 2000,
                Automatic = true
            },
            new DepositPlanDetail()
            {
                Id = 3,
                Profit = 4,
                Duration = 20,
                Min = 2500,
                Max = 6000,
                Automatic = true
            },
            new DepositPlanDetail()
            {
                Id = 4,
                Profit = 5,
                Duration = 30,
                Min = 10000,
                Max = 20000,
                Automatic = true
            },
            new DepositPlanDetail()
            {
                Id=5,
                Profit = 6,
                Duration = 35,
                Min = 20000,
                Max = 35000,
                Automatic = true
            },
        };
    }

    
}
