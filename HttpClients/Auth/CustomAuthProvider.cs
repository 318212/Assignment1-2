using HttpClients.ClientInterfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Auth
{
    public class CustomAuthProvider : AuthenticationStateProvider
    {
        private readonly IAuthService authService;

        public CustomAuthProvider(IAuthService authService)
        {
            this.authService = authService;
            authService.OnAuthStateChanged += AuthStateChanged;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsPrincipal principal = await authService.GetAuthAsync();

            return new AuthenticationState(principal);
        }

        private void AuthStateChanged(ClaimsPrincipal principal)
        {
            NotifyAuthenticationStateChanged(
                Task.FromResult(
                    new AuthenticationState(principal)
                )
            );
        }
    }
}