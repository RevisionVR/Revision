using Revision.Domain.Commons;
using Revision.Domain.Entities.Educations;

namespace Revision.Domain.Entities.Chats.Rooms;

public class EducationRoom : Auditable
{
    public long EducationId { get; set; }
    public Education Education { get; set; }
}