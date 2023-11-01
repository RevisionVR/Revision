using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Configurations;
using Revision.Domain.Entities.Educations;
using Revision.Domain.Entities.Users;
using Revision.Service.Commons.Helpers;
using Revision.Service.DTOs.Educations;
using Revision.Service.Exceptions;
using Revision.Service.Extensions;
using Revision.Service.Interfaces.Addresses;
using Revision.Service.Interfaces.Educations;
using Revision.Service.Validations.Educations;
using Revision.Service.Validations.Educations.Categories;

namespace Revision.Service.Services.Educations;

public class EducationService : IEducationService
{
    private readonly IMapper _mapper;
    private readonly IAddressService _addressService;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Education> _educationRepository;
    private readonly IRepository<EducationCategory> _categoryRepository;
    public EducationService(
        IAddressService addressService,
        IRepository<User> userRepository,
        IRepository<EducationCategory> categoryRepository,
        IMapper mapper, IRepository<Education> educationRepository)
    {
        _mapper = mapper;
        _addressService = addressService;
        _userRepository = userRepository;
        _categoryRepository = categoryRepository;
        _educationRepository = educationRepository;
    }

    public async Task<EducationResultDto> CreateAsync(EducationCreationDto dto)
    {
        var validation = new EducationCreationDtoValidator();
        var result = validation.Validate(dto);
        if (!result.IsValid)
            throw new RevisionException(400, result.Errors.FirstOrDefault().ToString());

        var existEducation = await _educationRepository.SelectAsync(
            education => education.Name.ToLower().Equals(dto.Name.ToLower()));
        if (existEducation is not null)
            throw new RevisionException(403, "This education already exists");

        var existUser = await _userRepository.SelectAsync(user => user.Id.Equals(dto.UserId))
            ?? throw new RevisionException(404, "This user is not found");

        var existCategory = await _categoryRepository.SelectAsync(
            category => category.Id.Equals(dto.EducationCategoryId))
            ?? throw new RevisionException(404, "This education category is not found");

        var mappedEducation = _mapper.Map<Education>(dto);
        mappedEducation.CreatedAt = TimeHelper.GetDateTime();
        mappedEducation.User = existUser;
        mappedEducation.EducationCategory = existCategory;

        if (dto.AddressCreationDto is not null)
            mappedEducation.Address = await _addressService.CreateAsync(dto.AddressCreationDto);

        await _educationRepository.AddAsync(mappedEducation);
        await _educationRepository.SaveAsync();

        return _mapper.Map<EducationResultDto>(mappedEducation);
    }

    public async Task<EducationResultDto> UpdateAsync(long id, EducationUpdateDto dto)
    {
        var validation = new EducationUpdateDtoValidator();
        var result = validation.Validate(dto);
        if (!result.IsValid)
            throw new RevisionException(400, result.Errors.FirstOrDefault().ToString());

        var existEducation = await _educationRepository.SelectAsync(education => education.Id.Equals(id),
            includes: new[] { "Address", "TopicPayments", "DevicePayments", "Devices" })
            ?? throw new RevisionException(404, "This education is not found");

        var existUser = await _userRepository.SelectAsync(user => user.Id.Equals(dto.UserId))
           ?? throw new RevisionException(404, "This user is not found");

        var existCategory = await _categoryRepository.SelectAsync(
            category => category.Id.Equals(dto.EducationCategoryId))
            ?? throw new RevisionException(404, "This education category is not found");

        if (existEducation.Address is not null)
            existEducation.Address = await _addressService.UpdateAsync(existEducation.Address.Id, dto.AddressUpdateDto);

        var mappedEducation = _mapper.Map(dto, existEducation);
        mappedEducation.UpdatedAt = TimeHelper.GetDateTime();
        mappedEducation.User = existUser;
        mappedEducation.EducationCategory = existCategory;

        _educationRepository.Update(mappedEducation);
        await _educationRepository.SaveAsync();

        return _mapper.Map<EducationResultDto>(mappedEducation);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existEducation = await _educationRepository.SelectAsync(education => education.Id.Equals(id),
            includes: new[] { "Address" })
           ?? throw new RevisionException(404, "This education is not found");

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
                "User",
                "Address",
                "Devices",
                "TopicPayments",
                "DevicePayments",
                "EducationCategory"
            })
           ?? throw new RevisionException(404, "This education is not found");

        return _mapper.Map<EducationResultDto>(existEducation);
    }

    public async Task<IEnumerable<EducationResultDto>> GetAllAsync(PaginationParams pagination)
    {
        var educations = await _educationRepository.SelectAll(
            includes: new[]
            {
                "User",
                "Address",
                "Devices",
                "TopicPayments",
                "DevicePayments",
                "EducationCategory"
            })
            .ToPaginate(pagination)
            .ToListAsync();

        return _mapper.Map<IEnumerable<EducationResultDto>>(educations);
    }
}