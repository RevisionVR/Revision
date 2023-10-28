using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Configurations;
using Revision.Domain.Entities.Subjects;
using Revision.Service.Commons.Helpers;
using Revision.Service.DTOs.Subjects;
using Revision.Service.Exceptions;
using Revision.Service.Extensions;
using Revision.Service.Interfaces.Subjects;
using Revision.Domain.Entities.Categories.SubjectCategories;

namespace Revision.Service.Services.Subjects;

public class SubjectService : ISubjectService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Subject> _subjectRepository;
    private readonly IRepository<SubjectCategory> _categoryRepository;
    public SubjectService(
        IMapper mapper, 
        IRepository<Subject> subjectRepository, 
        IRepository<SubjectCategory> categoryRepository)
    {
        _mapper = mapper;
        _subjectRepository = subjectRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<SubjectResultDto> CreateAsync(SubjectCreationDto dto)
    {
        var existSubject = await _subjectRepository.SelectAsync(subject => subject.Name.ToLower().Equals(dto.Name));
        if (existSubject is not null)
            throw new RevisionException(403, "This subject already exists");

        var existCategory = await _categoryRepository.SelectAsync(category => category.Id.Equals(dto.SubjectCategoryId))
            ?? throw new RevisionException(404, "This subject category is not found");

        var mappedSubject = _mapper.Map<Subject>(dto);
        mappedSubject.SubjectCategory = existCategory;
        mappedSubject.CreatedAt = TimeHelper.GetDateTime();

        await _subjectRepository.AddAsync(mappedSubject);
        await _subjectRepository.SaveAsync();

        return _mapper.Map<SubjectResultDto>(mappedSubject);
    }
    
    public async Task<SubjectResultDto> UpdateAsync(long id, SubjectUpdateDto dto)
    {
        var existSubject = await _subjectRepository.SelectAsync(subject => subject.Id.Equals(id), 
            includes: new[] { "Topics" })
            ?? throw new RevisionException(404, "This subject is not found");

        var existCategory = await _categoryRepository.SelectAsync(category => category.Id.Equals(dto.SubjectCategoryId))
            ?? throw new RevisionException(404, "This subject category is not found");

        var mappedSubject = _mapper.Map(dto, existSubject);
        mappedSubject.Id = id;
        mappedSubject.SubjectCategory = existCategory;
        mappedSubject.UpdatedAt = TimeHelper.GetDateTime();

        _subjectRepository.Update(mappedSubject);
        await _subjectRepository.SaveAsync();

        return _mapper.Map<SubjectResultDto>(mappedSubject);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existSubject = await _subjectRepository.SelectAsync(subject => subject.Id.Equals(id))
            ?? throw new RevisionException(404, "This subject is not found");

        _subjectRepository.Delete(existSubject);
        await _subjectRepository.SaveAsync();
        return true;
    }

    public async Task<bool> DestroyAsync(long id)
    {
        var existSubject = await _subjectRepository.SelectAsync(subject => subject.Id.Equals(id))
            ?? throw new RevisionException(404, "This subject is not found");

        _subjectRepository.Destroy(existSubject);
        await _subjectRepository.SaveAsync();
        return true;
    }

    public async Task<SubjectResultDto> GetByIdAsync(long id)
    {
        var existSubject = await _subjectRepository.SelectAsync(subject => subject.Id.Equals(id),
            includes: new[] { "SubjectCategory", "Topics" })
            ?? throw new RevisionException(404, "This subject is not found");

        return _mapper.Map<SubjectResultDto>(existSubject);
    }

    public async Task<IEnumerable<SubjectResultDto>> GetAllAsync(PaginationParams pagination)
    {
        var subjects = await _subjectRepository.SelectAll(includes: new[] { "SubjectCategory", "Topics" })
            .ToPaginate(pagination)
            .ToListAsync();

        return _mapper.Map<IEnumerable<SubjectResultDto>>(subjects);    
    }
}