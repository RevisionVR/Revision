namespace Revision.Service.DTOs.Countries;

public class CountryResultDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
}