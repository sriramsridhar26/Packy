using PackyAPI.Models;

namespace PackyAPI.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginDTO userDTO);
        Task<string> CreateToken();

    }
}
