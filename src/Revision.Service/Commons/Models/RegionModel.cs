using Newtonsoft.Json;

namespace Revision.Service.Commons.Models;

public class RegionModel
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("country_id")]
    public long CountryId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
}