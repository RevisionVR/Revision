using Revision.Service.DTOs.Addresses;
using Revision.Service.DTOs.EducationCategories;
using Revision.Service.DTOs.Users;

namespace Revision.Service.DTOs.Educations;

public class EducationResultDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? Number { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; }
    public UserResultDto User { get; set; }
    public EducationCategoryResultDto EducationCategory { get; set; }
    public AddressResultDto AddressResultDto { get; set; }
}