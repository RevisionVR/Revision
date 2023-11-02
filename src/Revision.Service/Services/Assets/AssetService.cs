using AutoMapper;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Assets;
using Revision.Service.Commons.Helpers;
using Revision.Service.DTOs.Assets;
using Revision.Service.Extensions;
using Revision.Service.Interfaces.Assets;
using System.Net.Mail;

namespace Revision.Service.Services.Assets;

public class AssetService : IAssetService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Asset> _repository;

    public async Task<Asset> UploadAsync(AssetCreationDto dto)
    {
        var webrootPath = Path.Combine(PathHelper.WebRootPath, "Files");

        if (!Directory.Exists(webrootPath))
            Directory.CreateDirectory(webrootPath);

        var fileExtension = Path.GetExtension(dto.FormFile.FileName);
        var fileName = $"{Guid.NewGuid().ToString("N")}{fileExtension}";
        var fullPath = Path.Combine(webrootPath, fileName);

        var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate);
        await fileStream.WriteAsync(dto.FormFile.ToByte());

        var createdAsset = new Asset
        {
            FileName = fileName,
            FilePath = fullPath
        };

        await _repository.AddAsync(createdAsset);
        await _repository.SaveAsync();

        return createdAsset;
    }

    public async Task<bool> DeleteAsync(Asset asset)
    {
        _repository.Delete(asset);
        await _repository.SaveAsync();
        return true;
    }
}