using System;
using BtcInvestmentInc.Client.Controls;
using BtcInvestmentInc.Server.Helper;
using BtcInvestmentInc.Server.Settings;
using BtcInvestmentInc.Shared.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Investment = BtcInvestmentInc.Server.Models.Investment;

namespace BtcInvestmentInc.Server.DatabaseContext
{
    public class UserDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private readonly IConfiguration _config;
        private AppSettings _appSettings;

        public UserDbContext(IConfiguration config, DbContextOptions<UserDbContext> c, IOptions<AppSettings> op) : base(c)
        {
            _config = config;
            _appSettings = op.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_config.GetConnectionString(_appSettings.Environment));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Investment> Investments { get; set; }
    }
}
