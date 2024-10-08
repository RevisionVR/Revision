﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Configurations;
using Revision.Domain.Entities.Educations;
using Revision.Domain.Entities.Users;
using Revision.Service.Commons.Helpers;
using Revision.Service.DTOs.UserEducations;
using Revision.Service.Exceptions;
using Revision.Service.Extensions;
using Revision.Service.Interfaces.Educations;

namespace Revision.Service.Services.Educations;

public class UserEducationService : IUserEducationService
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<UserEducation> _repository;
    private readonly IRepository<Education> _educationRepository;
    public UserEducationService(
        IMapper mapper,
        IRepository<User> userRepository,
        IRepository<UserEducation> repository,
        IRepository<Education> educationRepository)
    {
        _mapper = mapper;
        _repository = repository;
        _userRepository = userRepository;
        _educationRepository = educationRepository;
    }

    public async Task<UserEducationResultDto> CreateAsync(UserEducationCreationDto dto)
    {
        var existUser = await _userRepository.SelectAsync(user => user.Id.Equals(dto.UserId))
            ?? throw new RevisionException(404, "This user is not found");

        var existEducation = await _educationRepository.SelectAsync(education => education.Id.Equals(dto.EducationId))
            ?? throw new RevisionException(404, "This education is not found");

        var userEducations = _repository.SelectAll(ue => ue.EducationId.Equals(dto.EducationId));
        if (userEducations.Any())
        {
            var userEducation = userEducations.FirstOrDefault(user => user.UserId.Equals(dto.UserId));
            if (userEducation is not null)
                throw new RevisionException(403, "This user already exists in your education");
        }

        var demo = await _repository.SelectAsync(ue => ue.UserId.Equals(dto.UserId));
        if (demo is not null)
            throw new RevisionException(403, "This user already exists");

        var mappedResult = _mapper.Map<UserEducation>(dto);
        mappedResult.CreatedAt = TimeHelper.GetDateTime();
        mappedResult.User = existUser;
        mappedResult.Education = existEducation;

        await _repository.AddAsync(mappedResult);
        await _repository.SaveAsync();

        return _mapper.Map<UserEducationResultDto>(mappedResult);
    }

    public async Task<UserEducationResultDto> GetByUserIdAsync(long userId)
    {
        var existUserEducation = await _repository.SelectAsync(ue => ue.UserId.Equals(userId),
            includes: new[] { "User", "Education" })
            ?? throw new RevisionException(404, "This user education is not found");

        return _mapper.Map<UserEducationResultDto>(existUserEducation);
    }

    public async Task<IEnumerable<UserEducationResultDto>> GetByEducationIdAsync(long educationId)
    {
        var existUsersEducation = await _repository.SelectAll(ue => ue.EducationId.Equals(educationId),
            includes: new[] { "User", "Education" }).ToListAsync();

        if (!existUsersEducation.Any())
            throw new RevisionException(404, "This education is not found");

        return _mapper.Map<IEnumerable<UserEducationResultDto>>(existUsersEducation);
    }

    public async Task<IEnumerable<UserEducationResultDto>> GetAllAsync(PaginationParams pagination, string search = null)
    {
        var userEducations = _repository.SelectAll(includes: new[] { "User", "Education" });
        if (!string.IsNullOrEmpty(search))
        {
            userEducations = userEducations.Where(user =>
                user.User.FirstName.ToLower().Contains(search.ToLower()) ||
                user.User.LastName.ToLower().Contains(search.ToLower()) ||
                user.Education.Name.ToLower().Contains(search.ToLower())
            );
        }

        var result = userEducations.ToPagedList(pagination);
        return _mapper.Map<IEnumerable<UserEducationResultDto>>(result);
    }
}
