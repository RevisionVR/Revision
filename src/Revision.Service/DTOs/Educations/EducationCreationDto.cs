using Revision.Service.DTOs.Addresses;

namespace Revision.Service.DTOs.Educations;

public class EducationCreationDto
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long EducationCategoryId { get; set; }
    public AddressCreationDto Address { get; set; }
}