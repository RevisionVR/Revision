using Revision.Domain.Entities.Assets;
using Revision.Service.DTOs.Assets;

namespace Revision.Service.Interfaces.Assets;

public interface IAssetService
{
    Task<Asset> UploadAsync(AssetCreationDto dto);
    Task<bool> DeleteAsync(Asset asset);
}