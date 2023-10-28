using Revision.Domain.Commons;
using Revision.Domain.Entities.Addresses;

namespace Revision.Domain.Entities.Users;

public class UserAddress : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }

    public long AddressId { get; set; }
    public Address Address { get; set; }
}