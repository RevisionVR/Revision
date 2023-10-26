using Revision.Domain.Enums;
using Revision.Domain.Commons;
using Revision.Domain.Entities.Addresses;

namespace Revision.Domain.Entities.Users;

public class User : Auditable
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleName {  get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public Role Role { get; set; }

    public long AddressId { get; set; }
    public Address Address { get; set; }
}
