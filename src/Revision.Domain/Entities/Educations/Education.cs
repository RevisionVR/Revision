using Revision.Domain.Commons;
using Revision.Domain.Entities.Categories.EducationCategories;
using Revision.Domain.Entities.Users;

namespace Revision.Domain.Entities.Educations;

public class Education : Auditable
{
    public string Name { get; set; } = string.Empty;
    public int? Number {  get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }


    public long EducationCategoryId {  get; set; }
    public EducationCategory EducationCategory { get; set; }
    // addres
}
