namespace Revision.Service.DTOs;

public class ResetPassword
{
    public string PhoneNumber { get; set; } = string.Empty;
    public int Code { get; set; }
}
