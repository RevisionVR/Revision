namespace Revision.Service.DTOs.Users;

public class UserLoginDto
{
    public string Phone { get; set; } = string.Empty;
    public string password { get; set; } = string.Empty;
    public string Email { get; set; }
}
