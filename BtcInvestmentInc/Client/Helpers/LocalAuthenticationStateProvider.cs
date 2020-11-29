using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BtcInvestmentInc.Client.Helpers
{
    public class LocalAuthenticationStateProvide:AuthenticationStateProvider
    {
        private readonly ILocalStorageService _storageService;
        private readonly HttpClient _client;

        public LocalAuthenticationStateProvide(ILocalStorageService storageService, HttpClient client)
        {
            _storageService = storageService;
            _client = client;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _storageService.GetItemAsync<string>("token");

            if (token != null)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "authorize");

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var rr = await _client.SendAsync(request);

                if (!rr.IsSuccessStatusCode)
                {
                    await _storageService.RemoveItemAsync("token");
                    return  new AuthenticationState(new ClaimsPrincipal());
                }



                var identity = new ClaimsIdentity(new[]
                {
                    new Claim("token", token),
                    new Claim("username",await _storageService.GetItemAsync<string>("username")), 
                }, "JWT");

                var user = new ClaimsPrincipal(identity);
                var state = new AuthenticationState(user);

                NotifyAuthenticationStateChanged(Task.FromResult(state));

                return state;
            }

            return new AuthenticationState(new ClaimsPrincipal());
        }

        public void NotifyAuthenticationStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
