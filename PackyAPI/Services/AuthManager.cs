using Microsoft.AspNetCore.Identity;
using PackyAPI.Data;
using PackyAPI.Models;

namespace PackyAPI.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthManager(UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public Task<string> CreateToken()
        {
            return null;
        }

        public async Task<bool> ValidateUser(LoginDTO userDTO)
        {
            var user = await _userManager.FindByNameAsync(userDTO.Email);
            return (user != null && await _userManager.CheckPasswordAsync(user, userDTO.Password));
        }
    }
}
