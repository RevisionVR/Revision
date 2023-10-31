using Newtonsoft.Json;

namespace Revision.Service.DTOs.Regions;

public class RegionCreationDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("country_id")]
    public long CountryId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
}