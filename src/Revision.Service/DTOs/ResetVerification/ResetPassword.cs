namespace Revision.Service.DTOs.ResetVerification;

public class ResetPassword
{
    public string Phone { get; set; } = string.Empty;
    public int Code { get; set; }
}
