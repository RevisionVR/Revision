namespace Revision.Service.Interfaces.Auth;

public interface IIdentityService
{
    public long Id { get; }
    public string RoleName { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Phone { get; }
    public string Email { get; }
}
