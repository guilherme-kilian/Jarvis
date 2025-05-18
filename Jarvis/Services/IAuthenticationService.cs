using Jarvis.Clients;
using Jarvis.Const;
using Jarvis.Exceptions;
using Jarvis.Models.Auth;
using Jarvis.Models.User;
using System.Security.Claims;

namespace Jarvis.Services
{
    public interface IAuthenticationService
    {
        event Action<ClaimsPrincipal>? UserChanged;

        ClaimsPrincipal CurrentUser { get; set; }

        Task AuthenticateAsync(RegisterUserFrontModel create);

        Task AuthenticateAsync(string email, string password);
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

        public async Task AuthenticateAsync(string email, string password)
        {
            if(await _client.CheckPasswordAsync(email, password))
            {
                var user = await _client.GetAsync(email);

                if (user is null) 
                    return;

                Authenticate(user);
            }
            else
            {
                throw new AuthenticationException("Usuário ou senha inválido");
            }
        }

        public async Task AuthenticateAsync(RegisterUserFrontModel create)
        {
            if (string.IsNullOrEmpty(create.Email) || string.IsNullOrEmpty(create.Password) || string.IsNullOrEmpty(create.Name))
                return;

            var model = new CreateUserModel
            {
                Email = create.Email,
                Name = create.Name,
                Password = create.Password,
            };

            var user = await _client.CreateUserAsync(model);

            Authenticate(user);
        }

        private void Authenticate(UserModel user)
        {
            var identitiy = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, user.Name),
                    new(ClaimTypes.Email, user.Email),
                    new(ClaimTypesCustom.ProfilePicture, user.PictureUrl ?? string.Empty),
                    new(ClaimTypesCustom.DailyMeta, user.DailyMeta.ToString()),
                    new Claim(ClaimTypes.Role, "User"),
                ]);

            _currentUser = new ClaimsPrincipal(identitiy);
        }
    }
}
