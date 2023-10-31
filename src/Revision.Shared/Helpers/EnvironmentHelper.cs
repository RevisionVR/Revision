using Newtonsoft.Json;

namespace Revision.Shared.Helpers;

public static class EnvironmentHelper
{
    [JsonProperty("CountriesFilePath")]
    public static string CountryPath { get; set; }

    [JsonProperty("RegionsFilePath")]
    public static string RegionPath { get; set; }

    [JsonProperty("DistrictsFilePath")]
    public static string DistrictPath { get; set; }
}
