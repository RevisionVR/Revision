using Newtonsoft.Json;

namespace Revision.Service.Commons.Models;

public class DistrictModel
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("region_id")]
    public long RegionId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
}
