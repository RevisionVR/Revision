using Newtonsoft.Json;

namespace Revision.Service.Commons.Models;

public class CountryModel
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("code")]
    public string CountryCode { get; set; } = string.Empty;
}