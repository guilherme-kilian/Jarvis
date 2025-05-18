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

        Task<UserModel> AuthenticateAsync(RegisterUserFrontModel create);

        Task<UserModel> AuthenticateAsync(string email, string password);
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

        public async Task<UserModel> AuthenticateAsync(string email, string password)
        {
            if(await _client.CheckPasswordAsync(email, password))
            {
                var user = await _client.GetAsync(email) 
                    ?? throw new InvalidOperationException("Falha ao realizar o login. Usuário não encontrado");

                Authenticate(user);

                return user;
            }
            else
            {
                throw new AuthenticationException("Usuário ou senha inválido");
            }
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

            var user = await _client.CreateUserAsync(model);

            Authenticate(user);

            return user;
        }

        private void Authenticate(UserModel user)
        {
            List<Claim> claims = [
                    new Claim(ClaimTypes.Name, user.Name),
                    new(ClaimTypes.Email, user.Email),
                    new(ClaimTypesCustom.ProfilePicture, user.PictureUrl ?? string.Empty),
                    new(ClaimTypesCustom.DailyMeta, user.DailyMeta.ToString()),
                    new Claim(ClaimTypes.Role, "User"),
                ];

            var identitiy = new ClaimsIdentity("external");

            identitiy.AddClaims(claims);

            _currentUser = new ClaimsPrincipal(identitiy);
        }
    }
}
