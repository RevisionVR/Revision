namespace Revision.Service.DTOs.Users;

public class UserResetPasswordDto
{
    public string NewPassword { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}
