using Jarvis.Clients;
using Jarvis.Const;
using Jarvis.Exceptions;
using Jarvis.Models.Auth;
using Jarvis.Models.User;
using System.Security.Claims;

namespace Jarvis.Services
{
    public class AuthenticatedUser
    {
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
    }

    public interface IAuthenticationService
    {
        Task<UserModel> AuthenticateAsync(RegisterUserFrontModel create);

        Task<UserModel> AuthenticateAsync(string email, string password);

        Task LogoutAsync();
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJarvisApiClient _client;
        private readonly AuthenticatedUser _autUser;

        public AuthenticationService(IJarvisApiClient client, AuthenticatedUser user)
        {
            _client = client;
            _autUser = user;
        }

        public async Task<UserModel> AuthenticateAsync(string email, string password)
        {
            var user = await _client.Users.CheckPasswordAsync(email, password) 
                ?? throw new AuthenticationException("Usuário ou senha inválido");

            Authenticate(user);

            return user;            
        }

        public async Task<UserModel> AuthenticateAsync(RegisterUserFrontModel create)
        {
            if (string.IsNullOrEmpty(create.Email) || string.IsNullOrEmpty(create.Password) || string.IsNullOrEmpty(create.Name))
                throw new InvalidOperationException("Email, senha e nome são obrigatórios");

            var model = new CreateUserModel
            {
                Email = create.Email,
                Name = create.Name,
                Password = create.Password,
            };

            var user = await _client.Users.CreateUserAsync(model);

            Authenticate(user);

            return user;
        }

        private void Authenticate(UserModel user)
        {
            List<Claim> claims = [
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new(ClaimTypes.Email, user.Email),
                    new(ClaimTypesCustom.ProfilePicture, user.ProfilePicture ?? string.Empty),
                    new(ClaimTypesCustom.DailyMeta, user.WinTheDayGoal.ToString()),
                    new Claim(ClaimTypes.Role, "User"),
                ];

            var identitiy = new ClaimsIdentity("external");

            identitiy.AddClaims(claims);

            _autUser.CurrentUser = new ClaimsPrincipal(identitiy);
        }

        public Task LogoutAsync()
        {
            _autUser.CurrentUser = new();
            return Task.CompletedTask;
        }
    }
}
