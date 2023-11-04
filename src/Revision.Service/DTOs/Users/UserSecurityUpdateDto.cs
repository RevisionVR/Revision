namespace Revision.Service.DTOs.Users;

public class UserSecurityUpdateDto
{
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ReturnNewPassword { get; set; } = string.Empty;
}
