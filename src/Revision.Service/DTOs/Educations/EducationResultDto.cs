using Revision.Service.DTOs.Addresses;
using Revision.Service.DTOs.EducationCategories;

namespace Revision.Service.DTOs.Educations;

public class EducationResultDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public EducationCategoryResultDto EducationCategory { get; set; }
    public AddressResultDto Address { get; set; }

    //public IEnumerable<DeviceResultDto> Devices { get; set; }
    //public IEnumerable<TopicPaymentResultDto> TopicPayments { get; set; }
    //public IEnumerable<DevicePaymentResultDto> DevicePayments { get; set; }
}