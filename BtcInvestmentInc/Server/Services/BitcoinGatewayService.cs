using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using BtcInvestmentInc.Server.Settings;
using BtcInvestmentInc.Shared.ViewModel;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BtcInvestmentInc.Server.Services
{
    public interface IBitCoinService
    {
        Task<string> GetAddressToDeposit(string userId, int investmentId);
    }

    public class BitCoinGatewayService : IBitCoinService
    {
        public string AddressToCall =
            "https://blockchainapi.org/api/receive";

        private readonly AppSettings _appSettings;
        public BitCoinGatewayService(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }

        public async Task<string> GetAddressToDeposit(string userId, int investmentId)
        {
            var query = HttpUtility.ParseQueryString(AddressToCall);

            query["method"] = "create";
            query["callback"] = $"{_appSettings.BaseAddress}/investment/verify/{userId}/{investmentId}";
            query["address"] = _appSettings.WalletAddress;

            var httpClient = new HttpClient();

            var rr = await httpClient.GetAsync($"{AddressToCall}?{query.ToString()}");

            var response = await rr.Content.ReadAsStringAsync();

            dynamic result = JsonConvert.DeserializeObject(response);

            return result.input_address;
        }

    }
}
