using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BtcInvestmentInc.Server.Services;
using BtcInvestmentInc.Server.Settings;
using BtcInvestmentInc.Shared.Models.Investment;
using BtcInvestmentInc.Shared.Models.User;
using BtcInvestmentInc.Shared.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace BtcInvestmentInc.Server.Controllers
{
    [Authorize]
    [Controller]
    [Route("api/user/")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;

        public UserController(IUserService userService, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
        }

        [HttpGet("authorize")]
        public async Task<IActionResult> Authorize()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationModel authModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(new UserManagerResponse()
                {
                    Message = "Can't Authenticate User! Invalid!",
                });

            var result = await _userService.Authenticate(authModel);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationModel r)
        {
            if (!ModelState.IsValid)
                return BadRequest(new UserManagerResponse()
                {
                    Message = "Can't Register User!",
                    Errors = ModelState.Values.SelectMany(e => e.Errors.Select(x => x.ErrorMessage)).ToList(),
                    IsSuccess = false
                });

            var result = await _userService.Register(r);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var profile = await _userService.GetUserProfile(User.FindFirst(ClaimTypes.Name).Value);

            if (profile == null)
                return BadRequest();

            return Ok(profile);
        }

        [HttpPost("updateprofile")]
        public async Task<IActionResult> UpdateProfile([FromBody] Profile p)
        {
            var response = await _userService.UpdateProfile(User.FindFirst(ClaimTypes.Name).Value, p);

            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("getinvestments")]
        public async Task<IActionResult> GetCurrentInvestment()
        {
            var investments = await _userService.GetInvestments(GetUserId());

            if (investments == null)
                return BadRequest();

            return Ok(investments);
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.Name).Value;
        }

        [HttpPost("makedeposit")]
        public async Task<IActionResult> MakeDeposit([FromBody] DepositDetail dd)
        {
            var result = await _userService.MakeDeposit(GetUserId(), dd);
            
            return Ok(result);
        }

        [HttpGet("confirmpayment")]
        public async Task<IActionResult> ConfirmPayment([FromQuery]int id)
        {
            if ((await _userService.ConfirmPayment(GetUserId(), id))) return Ok();

            return BadRequest();
        }
    }
}
