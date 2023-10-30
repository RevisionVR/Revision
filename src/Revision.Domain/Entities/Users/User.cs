using Revision.Domain.Enums;
using Revision.Domain.Commons;

namespace Revision.Domain.Entities.Users;

public class User : Auditable
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public Role Role { get; set; }
}