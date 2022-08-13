using Microsoft.AspNetCore.Identity;
using YourSoft.BLL.Models.User;

namespace YourSoft.BLL.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);
        Task<AuthResponseDto> Login(LoginDto loginDto);
        Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
    }
}
