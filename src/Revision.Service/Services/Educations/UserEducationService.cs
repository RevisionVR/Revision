using AutoMapper;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Educations;
using Revision.Service.DTOs.UserEducations;
using Revision.Service.Interfaces.Educations;

namespace Revision.Service.Services.Educations;

public class UserEducationService : IUserEducationService
{
    private readonly IMapper _mapper;
    private readonly IRepository<UserEducation> _repository;

    public Task<UserEducationResultDto> CreateAsync(UserEducationCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(long userEducation)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserEducationResultDto>> GetByEducationIdAsync(long educationId)
    {
        throw new NotImplementedException();
    }

    public Task<UserEducationResultDto> GetByUserIdAsync(long userId)
    {
        throw new NotImplementedException();
    }

    public Task<UserEducationResultDto> UpdateAsync(UserEducationUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}
