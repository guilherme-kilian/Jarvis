using Jarvis.Exceptions;
using Jarvis.Models.User;

namespace Jarvis.Clients
{
    public interface IUserClient
    {
        Task<UserModel> GetAsync(string email);
        Task<UserModel> CreateUserAsync(CreateUserModel create);
        Task<bool> CheckPasswordAsync(string email, string password);
    }

    public class FakeUserClient : IUserClient
    {
        private static List<UserModel> Users = [];

        public Task<bool> CheckPasswordAsync(string email, string password)
        {
            var user = Users.FirstOrDefault(u => u.Email == email);

            if (user is null)
                return Task.FromResult(false);

            return Task.FromResult(user.Password == password);
        }

        public Task<UserModel> CreateUserAsync(CreateUserModel create)
        {
            var user = new UserModel()
            {
                Email = create.Email,
                Name = create.Name,
                Password = create.Password,
            };

            Users.Add(user);

            return Task.FromResult(user);
        }

        public Task<UserModel> GetAsync(string email)
        {
            var user = Users.FirstOrDefault(u => u.Email == email) 
                ?? throw new NotFoundException("Usuário não encontrado");

            return Task.FromResult(user);
        }
    }

    public class UserClient : IUserClient
    {
        public Task<bool> CheckPasswordAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> CreateUserAsync(CreateUserModel create)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetAsync(string email)
        {
            throw new NotImplementedException();
        }

        Task<bool> IUserClient.CheckPasswordAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
