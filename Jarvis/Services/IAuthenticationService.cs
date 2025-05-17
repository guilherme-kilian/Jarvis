using Jarvis.Clients;
using Jarvis.Exceptions;
using System.Security.Claims;

namespace Jarvis.Services
{
    public interface IAuthenticationService
    {
        Task AuthenticateAsync(string username, string password);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationClient _client;

        public event Action<ClaimsPrincipal>? UserChanged;

        private ClaimsPrincipal? _currentUser;

        public ClaimsPrincipal CurrentUser
        {
            get { return _currentUser ?? new(); }
            set
            {
                _currentUser = value;

                if (UserChanged is not null)
                {
                    UserChanged(_currentUser);
                }
            }
        }

        public AuthenticationService(IAuthenticationClient client)
        {
            _client = client;
        }

        public async Task AuthenticateAsync(string username, string password)
        {
            if(await _client.CheckPasswordAsync(username, password))
            {
                var identitiy = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "User")
                });

                _currentUser = new ClaimsPrincipal(identitiy);
            }
            else
            {
                throw new AuthenticationException("Usuário ou senha inválido");
            }
        }
    }
}
