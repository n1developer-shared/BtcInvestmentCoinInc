using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BtcInvestmentInc.Server.DatabaseContext;
using BtcInvestmentInc.Server.Helper;
using BtcInvestmentInc.Server.Models;
using BtcInvestmentInc.Server.Settings;
using BtcInvestmentInc.Shared.Models.Investment;
using BtcInvestmentInc.Shared.Models.User;
using BtcInvestmentInc.Shared.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace BtcInvestmentInc.Server.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBitCoinService _bitCoinService;
        private readonly UserDbContext _db;
        private readonly AppSettings _appSettings;

        public UserService(UserManager<ApplicationUser> userManager, IOptions<AppSettings> appSettings, IBitCoinService bitCoinService, UserDbContext db)
        {
            _userManager = userManager;
            _bitCoinService = bitCoinService;
            _db = db;
            _appSettings = appSettings.Value;
        }

        public async Task<UserManagerResponse> Authenticate(AuthenticationModel userModel)
        {
            if (userModel == null)
                return null;

            var user = await _userManager.FindByNameAsync(userModel.Username);

            if (user == null)
                return new UserManagerResponse()
                {
                    Message = "Unable to authenticate"
                };

            var result = await _userManager.CheckPasswordAsync(user, userModel.Password);

            if (!result)
                return new UserManagerResponse()
                {
                    Message = "Unable to authenticate"
                };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new UserManagerResponse()
            {
                IsSuccess = true,
                Message = tokenString+"|"+user.UserName
            };
        }

        public async Task<UserManagerResponse> Register(RegistrationModel r)
        {
            if (r == null)
                return null;

            var iu = new ApplicationUser()
            {
                UserName = r.Username,
                Email = r.Email
            };

            var result = await _userManager.CreateAsync(iu, r.Password);

            if (result.Succeeded)
            {
                return new UserManagerResponse()
                {
                    IsSuccess = true,
                    Message = "Registration successful"
                };
            }

            return new UserManagerResponse()
            {
                Errors = result.Errors.Select(r => r.Description).ToList(),
                IsSuccess = false,
                Message = "User did't created!"
            };
        }

        public async Task<Profile> GetUserProfile(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            return new Profile() { BtcWalletAddress = user.BtcWalletAddress };
        }

        public async Task<UserManagerResponse> UpdateProfile(string userId, Profile profile)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return new UserManagerResponse()
                {
                    Message = "User Not Found!",
                    IsSuccess = false
                };

            user.BtcWalletAddress = profile.BtcWalletAddress;

            await _userManager.UpdateAsync(user);

            return new UserManagerResponse()
            {
                Message = "Updated",
                IsSuccess = true
            };
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        
        public async Task<List<InvestmentDetail>> GetInvestments(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return null;

            var invests = _db.Investments.Where(i => i.Confirmed && i.UserId.Equals(user.Id.ToString()));

            return invests.Select(i => new InvestmentDetail()
            {
                Date = i.Date,
                Payment = i.Payment,
                PlanId = i.PlanId
            }).ToList();
        }

        public async Task<UserManagerResponse> MakeDeposit(string id, DepositDetail dd)
        {
            var localDP = DepositPlanDetail.CurrentPlans.FirstOrDefault(d => d.Id == dd.Id);

            //return new UserManagerResponse()
            //{
            //    IsSuccess =  false,
            //    Message=  JsonConvert.SerializeObject(localDP)
            //};

            var response = new UserManagerResponse()
            {
                IsSuccess = false
            };

            if (localDP == null)
            {
                response.Message = "Plan not found!";
                return response;
            }

            if (!(dd.Amount >= localDP.Min && dd.Amount <= localDP.Max))
            {
                response.Message = "Amount can't be greater than or less than package ";
                return response;
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return null;

            var investment = new Investment()
            {
                PlanId = dd.Id,
                Confirmed = false,
                UserId = user.Id.ToString()
            };

            await _db.Investments.AddAsync(investment);

            await _db.SaveChangesAsync();

            response.Message = await _bitCoinService.GetAddressToDeposit(user.Id.ToString(), investment.Id)+"|"+investment.Id;
            response.IsSuccess = true;

            return response;
        }

        public async Task<bool> ConfirmPayment(string userId, int investmentId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) return false;

            var i = await _db.Investments.FirstOrDefaultAsync(i => i.Id == investmentId && i.UserId.Equals(userId));

            if (i == null) return false;

            return i.Confirmed;
        }
    }
}
