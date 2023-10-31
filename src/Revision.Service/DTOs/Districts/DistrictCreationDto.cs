using Newtonsoft.Json;

namespace Revision.Service.DTOs.Districts;

public class DistrictCreationDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("region_id")]
    public long RegionId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
}