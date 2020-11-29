using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BtcInvestmentInc.Shared.ViewModel
{
    public class UserManagerResponse
    {
        public UserManagerResponse()
        {
            IsSuccess = false;
        }

        public string Message { get; set; }
        public bool IsSuccess { get; set; }

        public List<string> Errors { get; set; }
    }
}
