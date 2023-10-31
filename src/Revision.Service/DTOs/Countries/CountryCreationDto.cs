﻿using Newtonsoft.Json;

namespace Revision.Service.DTOs.Countries;

public class CountryCreationDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("code")]
    public string CountryCode { get; set; } = string.Empty;
}