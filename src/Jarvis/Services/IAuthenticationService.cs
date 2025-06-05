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
        Task<FullUserModel> AuthenticateAsync(RegisterUserFrontModel create);
        
        Task<FullUserModel> AuthenticateAsync(string token);

        Task<FullUserModel> AuthenticateAsync(LoginModel login);

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

        public async Task<FullUserModel> AuthenticateAsync(LoginModel login)
        {
            var token = await _client.Users.CheckPasswordAsync(login);
            return await AuthenticateAsync(token.Token);
        }

        public async Task<FullUserModel> AuthenticateAsync(string token)
        {
            var user = await _client.Users.RawGetAsync(token)
                ?? throw new AuthenticationException("Usuário ou senha inválido");

            var fullUser = MapUser(user, token);

            Authenticate(fullUser);

            return fullUser;
        }

        public async Task<FullUserModel> AuthenticateAsync(RegisterUserFrontModel create)
        {
            if (string.IsNullOrEmpty(create.Email) || string.IsNullOrEmpty(create.Password) || string.IsNullOrEmpty(create.Name))
                throw new InvalidOperationException("Email, senha e nome são obrigatórios");

            var nameParts = create.Name.Split(" ");

            if(nameParts.Length < 2)
                throw new InvalidOperationException("Nome incompleto");

            var firstName = nameParts.First();

            var lastName = string.Join(" ", nameParts.Skip(1));

            var model = new CreateUserModel
            {
                Email = create.Email,
                Name = firstName,
                LastName = lastName,
                Password = create.Password,
            };

            var user = await _client.Users.CreateUserAsync(model);

            var token = await _client.Users.CheckPasswordAsync(new() { Email = create.Email, Password = create.Password});

            var fullUser = MapUser(user, token.Token);

            Authenticate(fullUser);            

            return fullUser;
        }

        private void Authenticate(FullUserModel user)
        {
            List<Claim> claims = [
                    //new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    //new(ClaimTypes.Email, user.Email),
                    new(ClaimTypesCustom.Token, user.Token),
                    new Claim(ClaimTypesCustom.LastName, user.LastName),
                    new(ClaimTypesCustom.ProfilePicture, user.ProfilePictureUrl ?? string.Empty),
                    new Claim(ClaimTypes.Role, "User"),
                ];

            var identitiy = new ClaimsIdentity("external");

            identitiy.AddClaims(claims);

            _autUser.CurrentUser = new ClaimsPrincipal(identitiy);
        }

        private static FullUserModel MapUser(UserModel user, string token)
        {
            return new FullUserModel
            {
                Name = user.Name,
                LastName = user.LastName,
                WinTheDayGoal = user.WinTheDayGoal,
                ProfilePictureUrl = user.ProfilePictureUrl ?? string.Empty,
                Token = token,                
            };
        }

        public Task LogoutAsync()
        {
            _autUser.CurrentUser = new();
            return Task.CompletedTask;
        }
    }
}
