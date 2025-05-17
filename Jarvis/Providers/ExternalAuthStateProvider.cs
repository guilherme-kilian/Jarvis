using Jarvis.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Providers
{
    public class ExternalAuthStateProvider : AuthenticationStateProvider
    {
        private AuthenticationState currentUser;

        public ExternalAuthStateProvider(AuthenticationService service)
        {
            currentUser = new AuthenticationState(service.CurrentUser);

            service.UserChanged += (newUser) =>
            {
                currentUser = new AuthenticationState(newUser);
                NotifyAuthenticationStateChanged(Task.FromResult(currentUser));
            };
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
            Task.FromResult(currentUser);
    }
}
