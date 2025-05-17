using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Clients
{
    public interface IAuthenticationClient
    {
        Task<bool> CheckPasswordAsync(string username, string password);
    }

    public class FakeAuthenticationClient : IAuthenticationClient
    {
        public Task<bool> CheckPasswordAsync(string username, string password)
        {
            return Task.FromResult(true);
        }
    }

    public class AuthenticationClient : IAuthenticationClient
    {
        public Task<bool> CheckPasswordAsync(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
