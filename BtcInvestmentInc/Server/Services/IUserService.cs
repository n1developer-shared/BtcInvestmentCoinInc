using System.Collections.Generic;
using System.Threading.Tasks;
using BtcInvestmentInc.Server.Helper;
using BtcInvestmentInc.Server.Models;
using BtcInvestmentInc.Shared.Models.Investment;
using BtcInvestmentInc.Shared.Models.User;
using BtcInvestmentInc.Shared.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace BtcInvestmentInc.Server.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> UpdateProfile(string userId, Profile profile);
        Task<Profile> GetUserProfile(string userId);
        Task<UserManagerResponse> Authenticate(AuthenticationModel userModel);
        Task<UserManagerResponse> Register(RegistrationModel newUser);
        Task<ApplicationUser> GetById(string id);
        Task<List<InvestmentDetail>> GetInvestments(string id);
        Task<UserManagerResponse> MakeDeposit(string id, DepositDetail dd);
        Task<bool> ConfirmPayment(string userId, int investmentId);
    }
}
