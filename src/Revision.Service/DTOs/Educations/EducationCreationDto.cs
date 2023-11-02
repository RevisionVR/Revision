using Revision.Service.DTOs.Addresses;

namespace Revision.Service.DTOs.Educations;

public class EducationCreationDto
{
    public string Name { get; set; } = string.Empty;
    public int? Number { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; }
    public long EducationCategoryId { get; set; }
    public AddressCreationDto AddressCreationDto { get; set; }
}