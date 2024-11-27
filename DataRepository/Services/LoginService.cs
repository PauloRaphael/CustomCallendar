using Microsoft.Extensions.Configuration;

namespace DataRepository.Services
{
    public class LoginService(IConfiguration configuration)
    {
        private readonly string _password = configuration["Password"];

        public bool Authorize(string password)
        {
            return password == _password;
        }
    }
}
