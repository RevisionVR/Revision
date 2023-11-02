using Revision.Domain.Commons;
using Revision.Domain.Entities.Users;

namespace Revision.Domain.Entities.Educations;

public class UserEducation : Auditable
{
    public long EducationId { get; set; }
    public Education Education { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
}