using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Configurations;
using Revision.Domain.Entities.Educations;
using Revision.Service.Commons.Helpers;
using Revision.Service.DTOs.EducationCategories;
using Revision.Service.Exceptions;
using Revision.Service.Extensions;
using Revision.Service.Interfaces.Educations;

namespace Revision.Service.Services.Educations;

public class EducationCategoryService : IEducationCategoryService
{
    private readonly IMapper _mapper;
    private readonly IRepository<EducationCategory> _repository;
    public EducationCategoryService(IMapper mapper, IRepository<EducationCategory> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<EducationCategoryResultDto> CreateAsync(EducationCategoryCreationDto dto)
    {
        var existCategory = await _repository.SelectAsync(
            category => category.Name.ToLower().Equals(dto.Name.ToLower()));
        if (existCategory is not null)
            throw new RevisionException(403, "This education category already exists");

        var mappedCategory = _mapper.Map<EducationCategory>(dto);
        mappedCategory.CreatedAt = TimeHelper.GetDateTime();

        await _repository.AddAsync(mappedCategory);
        await _repository.SaveAsync();

        return _mapper.Map<EducationCategoryResultDto>(mappedCategory);
    }

    public async Task<EducationCategoryResultDto> UpdateAsync(long id, EducationCategoryUpdateDto dto)
    {
        var existCategory = await _repository.SelectAsync(category => category.Id.Equals(id),
            includes: new[] { "Educations" })
            ?? throw new RevisionException(403, "This education category is not found");

        var mappedCategory = _mapper.Map(dto, existCategory);
        mappedCategory.Id = id;
        mappedCategory.UpdatedAt = TimeHelper.GetDateTime();

        _repository.Update(mappedCategory);
        await _repository.SaveAsync();

        return _mapper.Map<EducationCategoryResultDto>(mappedCategory);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existCategory = await _repository.SelectAsync(category => category.Id.Equals(id))
            ?? throw new RevisionException(403, "This education category is not found");

        _repository.Delete(existCategory);
        await _repository.SaveAsync();
        return true;
    }

    public async Task<bool> DestroyAsync(long id)
    {
        var existCategory = await _repository.SelectAsync(category => category.Id.Equals(id))
            ?? throw new RevisionException(403, "This education category is not found");

        _repository.Destroy(existCategory);
        await _repository.SaveAsync();
        return true;
    }

    public async Task<EducationCategoryResultDto> GetByIdAsync(long id)
    {
        var existCategory = await _repository.SelectAsync(category => category.Id.Equals(id),
            includes: new[] { "Educations" })
            ?? throw new RevisionException(403, "This education category is not found");

        return _mapper.Map<EducationCategoryResultDto>(existCategory);
    }

    public async Task<IEnumerable<EducationCategoryResultDto>> GetAllAsync()
    {
        var categories = await _repository.SelectAll(includes: new[] { "Educations" }).ToListAsync();
        return _mapper.Map<IEnumerable<EducationCategoryResultDto>>(categories);
    }

    public async Task<IEnumerable<EducationCategoryResultDto>> GetAllAsync(PaginationParams pagination, string search = null)
    {
        var categories = _repository.SelectAll(includes: new[] { "Educations" });
        if (!string.IsNullOrEmpty(search))
        {
            categories = categories.Where(category =>
            category.Name.ToLower().Contains(search.ToLower()));
        }

        var result = categories.ToPagedList(pagination);
        return _mapper.Map<IEnumerable<EducationCategoryResultDto>>(result);
    }
}