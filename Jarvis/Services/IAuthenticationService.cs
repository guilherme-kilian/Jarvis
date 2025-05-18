using Jarvis.Clients;
using Jarvis.Exceptions;
using Jarvis.Models.Auth;
using System.Security.Claims;

namespace Jarvis.Services
{
    public interface IAuthenticationService
    {
        event Action<ClaimsPrincipal>? UserChanged;

        ClaimsPrincipal CurrentUser { get; set; }

        Task AuthenticateAsync(RegisterUserFrontModel create);

        Task AuthenticateAsync(string username, string password);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserClient _client;

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

        public AuthenticationService(IUserClient client)
        {
            _client = client;
        }

        public async Task AuthenticateAsync(string username, string password)
        {
            if(await _client.CheckPasswordAsync(username, password))
            {
                _client.Get
                AuthenticateAsync(username);
            }
            else
            {
                throw new AuthenticationException("Usuário ou senha inválido");
            }
        }

        public Task AuthenticateAsync(RegisterUserFrontModel create)
        {
            throw new NotImplementedException();
        }

        private void AuthenticateAsync(string username)
        {
            var identitiy = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "User")
                });

            _currentUser = new ClaimsPrincipal(identitiy);
        }
    }
}
