namespace Revision.Service.DTOs.ResetVerification;

public class ResetPasswordDto
{
    public string Phone { get; set; } = string.Empty;
    public int Code { get; set; }
}