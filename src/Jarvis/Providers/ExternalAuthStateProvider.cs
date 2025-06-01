using Jarvis.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace Jarvis.Providers
{
    public class ExternalAuthStateProvider : AuthenticationStateProvider
    {
        private AuthenticationState _currentUser;

        public ExternalAuthStateProvider(AuthenticatedUser authUser)
        {
            _currentUser = new AuthenticationState(authUser.CurrentUser);

            authUser.UserChanged += (newUser) =>
            {
                _currentUser = new AuthenticationState(newUser);
                NotifyAuthenticationStateChanged(Task.FromResult(_currentUser));
            };
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
            Task.FromResult(_currentUser);
    }
}
