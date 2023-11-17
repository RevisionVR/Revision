using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Configurations;
using Revision.Domain.Entities.Subjects;
using Revision.Service.Commons.Helpers;
using Revision.Service.DTOs.SubjectCategories;
using Revision.Service.Exceptions;
using Revision.Service.Extensions;
using Revision.Service.Interfaces.Subjects;

namespace Revision.Service.Services.Subjects;

public class SubjectCategoryService : ISubjectCategoryService
{
    private readonly IMapper _mapper;
    private readonly IRepository<SubjectCategory> _repository;
    public SubjectCategoryService(IMapper mapper, IRepository<SubjectCategory> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<SubjectCategoryResultDto> CreateAsync(SubjectCategoryCreationDto dto)
    {
        var existCategory = await _repository.SelectAsync(
            category => category.Name.ToLower().Equals(dto.Name.ToLower()));
        if (existCategory is not null)
            throw new RevisionException(403, "This subject category already exists");

        var mappedCategory = _mapper.Map<SubjectCategory>(dto);
        mappedCategory.CreatedAt = TimeHelper.GetDateTime();

        await _repository.AddAsync(mappedCategory);
        await _repository.SaveAsync();

        return _mapper.Map<SubjectCategoryResultDto>(mappedCategory);
    }

    public async Task<SubjectCategoryResultDto> UpdateAsync(long id, SubjectCategoryUpdateDto dto)
    {
        var existCategory = await _repository.SelectAsync(category => category.Id.Equals(id),
            includes: new[] { "Subjects" })
            ?? throw new RevisionException(404, "This subject category is not found");

        var mappedCategory = _mapper.Map(dto, existCategory);
        mappedCategory.Id = id;
        mappedCategory.UpdatedAt = TimeHelper.GetDateTime();

        _repository.Update(mappedCategory);
        await _repository.SaveAsync();

        return _mapper.Map<SubjectCategoryResultDto>(mappedCategory);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existCategory = await _repository.SelectAsync(category => category.Id.Equals(id))
            ?? throw new RevisionException(404, "This subject category is not found");

        _repository.Delete(existCategory);
        await _repository.SaveAsync();
        return true;
    }

    public async Task<bool> DestroyAsync(long id)
    {
        var existCategory = await _repository.SelectAsync(category => category.Id.Equals(id))
            ?? throw new RevisionException(404, "This subject category is not found");

        _repository.Destroy(existCategory);
        await _repository.SaveAsync();
        return true;
    }

    public async Task<SubjectCategoryResultDto> GetByIdAsync(long id)
    {
        var existCategory = await _repository.SelectAsync(category => category.Id.Equals(id),
            includes: new[] { "Subjects" })
            ?? throw new RevisionException(404, "This subject category is not found");

        return _mapper.Map<SubjectCategoryResultDto>(existCategory);
    }

    public async Task<IEnumerable<SubjectCategoryResultDto>> GetAllAsync()
    {
        var categories = await _repository.SelectAll(includes: new[] { "Subjects" })
            .ToListAsync();

        return _mapper.Map<IEnumerable<SubjectCategoryResultDto>>(categories);
    }

    public async Task<IEnumerable<SubjectCategoryResultDto>> GetAllAsync(PaginationParams pagination, string search = null)
    {
        var categories = _repository.SelectAll(includes: new[] { "Subjects" });
        if (!string.IsNullOrEmpty(search))
        {
            categories = categories.Where(category =>
            category.Name.ToLower().Equals(search.ToLower()));
        }

        var result = categories.ToPagedList(pagination);
        return _mapper.Map<IEnumerable<SubjectCategoryResultDto>>(result);
    }
}