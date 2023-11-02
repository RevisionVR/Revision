using Microsoft.AspNetCore.Http;

namespace Revision.Service.DTOs.Assets;

public class AssetCreationDto
{
    public IFormFile FormFile { get; set; }
}