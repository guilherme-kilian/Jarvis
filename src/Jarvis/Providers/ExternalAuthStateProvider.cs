using Jarvis.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace Jarvis.Providers
{
    public class ExternalAuthStateProvider : AuthenticationStateProvider
    {
        private AuthenticationState _currentUser;

        public ExternalAuthStateProvider(IAuthenticationService service)
        {
            _currentUser = new AuthenticationState(service.CurrentUser);

            service.UserChanged += (newUser) =>
            {
                _currentUser = new AuthenticationState(newUser);
                NotifyAuthenticationStateChanged(Task.FromResult(_currentUser));
            };
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
            Task.FromResult(_currentUser);
    }
}
