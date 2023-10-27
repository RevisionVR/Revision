using Revision.Domain.Enums;

namespace Revision.Service.DTOs.Users;

public class UserCreationDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public Role Role { get; set; }
    public long AddressId { get; set; }
}