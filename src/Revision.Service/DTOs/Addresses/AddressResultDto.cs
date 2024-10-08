﻿using Revision.Service.DTOs.Countries;
using Revision.Service.DTOs.Districts;
using Revision.Service.DTOs.Regions;

namespace Revision.Service.DTOs.Addresses;

public class AddressResultDto
{
    public long Id { get; set; }
    public string Home { get; set; } = string.Empty;
    public float? Longitude { get; set; }
    public float? Latitude { get; set; }
    public CountryResultDto Country { get; set; }
    public RegionResultDto Region { get; set; }
    public DistrictResultDto District { get; set; }
}