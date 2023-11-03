using Revision.Service.DTOs.Users;

namespace Revision.Service.Interfaces.Auth;

public interface IAuthService
{
    Task<(bool Result, string Token)> RegisterAsync(UserCreationDto dto);
    //public Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegister(string phoneAdmin);
    //public Task<(bool Result, string Token)> VerifyRegisterAsync(string phoneAdmin, int code);
    Task<(bool Result, string Token)> LoginAsync(UserLoginDto dto);
    Task<(bool Result, int CachedMinutes)> ResetPasswordAsync(UserResetPasswordDto dto);
    Task<(bool Result, string Token)> VerifyResetPasswordAsync(string phone, int code);
}