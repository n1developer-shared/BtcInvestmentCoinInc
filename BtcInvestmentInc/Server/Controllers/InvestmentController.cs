using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BtcInvestmentInc.Server.DatabaseContext;
using BtcInvestmentInc.Server.Helper;
using BtcInvestmentInc.Server.Models;
using BtcInvestmentInc.Server.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BtcInvestmentInc.Server.Controllers
{
    [Controller]
    [Route("investment")]
    [AllowAnonymous]
    public class InvestmentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserDbContext _dbContext;

        public InvestmentController(UserManager<ApplicationUser> userManager, UserDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet("verify/{userId}/{investmentId:int}")]
        public async Task<IActionResult> Verify([FromRoute] string userId, [FromRoute] int investmentId)
        {
            ApplicationUser user = null;
            
            try
            {
                user = await _userManager.FindByIdAsync(userId);
            }
            catch (Exception e)
            {

            }

            if (user == null)
                return BadRequest();

            var investments =
                _dbContext.Investments.FirstOrDefault(i => i.UserId.Equals(userId) && i.Id == investmentId);

            if (investments != null)
            {
                investments.Confirmed = true;
                investments.Date = DateTime.Now;
                
                await _userManager.UpdateAsync(user);
                await _dbContext.SaveChangesAsync();
                return Ok("*ok*");
            }

            return BadRequest("Not Found");
        }
    }
}
