namespace BtcInvestmentInc.Server.Settings
{
    public class AppSettings
    {
        public string JwtSecret { get; set; }
        public string WalletAddress { get; set; }
        public string EncryptPassword { get; set; }
        public string BaseAddress { get; set; }
        public string Environment { get; set; }
    }
}
