using Revision.Domain.Commons;
using Revision.Domain.Entities.Addresses;
using Revision.Domain.Entities.Categories;
using Revision.Domain.Entities.Users;

namespace Revision.Domain.Entities.Educations;

public class Education : Auditable
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }

    public long EducationCategoryId { get; set; }
    public EducationCategory EducationCategory { get; set; }
}
