using Newtonsoft.Json.Linq;
using Revision.Service.DTOs.Users;

namespace Revision.Service.Interfaces.Auth;

public interface IAuthService
{
    public Task<(bool Result, string token)> RegisterAsync(UserCreationDto dto);
    //public Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegister(string phoneAdmin);
    //public Task<(bool Result, string token)> VerifyRegisterAsync(string phoneAdmin, int code);
    public Task<(bool Result, string token)> LoginAsync(UserLoginDto dto);
    public Task<(bool Result, int CachedMinutes)> ResetPasswordAsync(UserResetPasswordDto dto);
    public Task<(bool Result, string Token)> VerifyResetPasswordAsync(string phone, int code);
}