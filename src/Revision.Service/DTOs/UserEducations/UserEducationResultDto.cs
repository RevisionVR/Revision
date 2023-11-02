using Revision.Service.DTOs.Educations;
using Revision.Service.DTOs.Users;

namespace Revision.Service.DTOs.UserEducations;

public class UserEducationResultDto
{
    public long Id { get; set; }
    public UserResultDto User { get; set; }
    public EducationResultDto Education { get; set; }
}
