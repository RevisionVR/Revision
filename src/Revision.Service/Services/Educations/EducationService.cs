using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Configurations;
using Revision.Domain.Entities.Educations;
using Revision.Service.Commons.Helpers;
using Revision.Service.DTOs.Educations;
using Revision.Service.Exceptions;
using Revision.Service.Extensions;
using Revision.Service.Interfaces.Addresses;
using Revision.Service.Interfaces.Educations;

namespace Revision.Service.Services.Educations;

public class EducationService : IEducationService
{
    private readonly IMapper _mapper;
    private readonly IAddressService _addressService;
    private readonly IRepository<Education> _educationRepository;
    private readonly IRepository<EducationCategory> _categoryRepository;
    public EducationService(
        IAddressService addressService,
        IRepository<EducationCategory> categoryRepository,
        IMapper mapper, IRepository<Education> educationRepository)
    {
        _mapper = mapper;
        _addressService = addressService;
        _categoryRepository = categoryRepository;
        _educationRepository = educationRepository;
    }

    public async Task<EducationResultDto> CreateAsync(EducationCreationDto dto)
    {
        var existEducation = await _educationRepository.SelectAsync(
            education => education.Phone.Equals(dto.Phone));
        if (existEducation is not null)
            throw new RevisionException(403, "This education already exists");

        var existCategory = await _categoryRepository.SelectAsync(
            category => category.Id.Equals(dto.EducationCategoryId))
            ?? throw new RevisionException(404, "This education category is not found");

        var mappedEducation = _mapper.Map<Education>(dto);
        mappedEducation.CreatedAt = TimeHelper.GetDateTime();
        mappedEducation.UpdatedAt = TimeHelper.GetDateTime();
        mappedEducation.EducationCategory = existCategory;

        if (dto.Address is not null)
        {
            var address = await _addressService.CreateAsync(dto.Address);
            mappedEducation.AddressId = address.Id;
            mappedEducation.Address = address;
        }

        await _educationRepository.AddAsync(mappedEducation);
        await _educationRepository.SaveAsync();

        var result = _mapper.Map<EducationResultDto>(mappedEducation);

        return result;
    }

    public async Task<EducationResultDto> UpdateAsync(long id, EducationUpdateDto dto)
    {
        var existEducation = await _educationRepository.SelectAsync(education => education.Id.Equals(id),
            includes: new[] { "Address", "UserEducation" })
            ?? throw new RevisionException(404, "This education is not found");

        var existCategory = await _categoryRepository.SelectAsync(
            category => category.Id.Equals(dto.EducationCategoryId))
            ?? throw new RevisionException(404, "This education category is not found");

        if (existEducation.Address is not null)
        {
            var address = await _addressService.UpdateAsync(existEducation.Address.Id, dto.Address);
            existEducation.Address = address;
            existEducation.Id = address.Id;
        }

        var mappedEducation = _mapper.Map(dto, existEducation);
        mappedEducation.UpdatedAt = TimeHelper.GetDateTime();
        mappedEducation.EducationCategory = existCategory;

        _educationRepository.Update(mappedEducation);
        await _educationRepository.SaveAsync();

        var result = _mapper.Map<EducationResultDto>(mappedEducation);

        return result;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existEducation = await _educationRepository.SelectAsync(education => education.Id.Equals(id),
            includes: new[] { "Address", "UserEducation" })
           ?? throw new RevisionException(404, "This education is not found");

        await _addressService.DeleteAsync(existEducation.Address.Id);

        _educationRepository.Delete(existEducation);
        await _educationRepository.SaveAsync();
        return true;
    }

    public async Task<EducationResultDto> GetByIdAsync(long id)
    {
        var existEducation = await _educationRepository.SelectAsync(
            education => education.Id.Equals(id),
            includes: new[]
            {
                "Address.Country",
                "Address.District",
                "Address.Region",
                "EducationCategory"
            })
           ?? throw new RevisionException(404, "This education is not found");

        return _mapper.Map<EducationResultDto>(existEducation);
    }

    public async Task<IEnumerable<EducationResultDto>> GetAllAsync()
    {
        var educations = await _educationRepository.SelectAll(
            includes: new[]
            {
                "Address.Country",
                "Address.District",
                "Address.Region",
                "EducationCategory"
            })
            .ToListAsync();

        return _mapper.Map<IEnumerable<EducationResultDto>>(educations);
    }

    public async Task<IEnumerable<EducationResultDto>> SearchAsync(string Item)
    {
        var resultDb = await _educationRepository.SelectAll().Where(edu => edu.Name.ToLower().Contains(Item)).ToListAsync();

        if (resultDb.Count == 0)
            throw new RevisionException(404, "This education is not found");

        return _mapper.Map<IEnumerable<EducationResultDto>>(resultDb);
    }
}