using Revision.Service.DTOs.Auth;
using Revision.Service.DTOs.Users;

namespace Revision.Service.Interfaces.Auth;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(UserCreationDto dto);
    //public Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegister(string phoneAdmin);
    //public Task<(bool Result, string Token)> VerifyRegisterAsync(string phoneAdmin, int code);
    Task<AuthResult> LoginAsync(UserLoginDto dto);
    Task<AuthResetPassword> ResetPasswordAsync(UserResetPasswordDto dto);
    Task<AuthResult> VerifyResetPasswordAsync(string phone, int code);
}