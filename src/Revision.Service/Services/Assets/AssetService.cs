using AutoMapper;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Assets;
using Revision.Service.DTOs.Assets;
using Revision.Service.Interfaces.Assets;

namespace Revision.Service.Services.Assets;

public class AssetService : IAssetService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Asset> _repository;

    public Task<bool> DeleteAsync(Asset asset)
    {
        throw new NotImplementedException();
    }

    public Task<Asset> UploadAsync(AssetCreationDto dto)
    {
        throw new NotImplementedException();
    }
}