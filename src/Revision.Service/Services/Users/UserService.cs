using AutoMapper;
using Revision.Domain.Enums;
using Revision.Service.DTOs.Users;
using Revision.Domain.Configurations;
using Revision.Service.Interfaces.Users;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Users;
using Revision.Service.Exceptions;
using Revision.Service.Commons.Security;
using Revision.Service.Extensions;
using Microsoft.EntityFrameworkCore;
using Revision.Service.Commons.Helpers;

namespace Revision.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _userRepository;
    public UserService(
        IMapper mapper, 
        IRepository<User> userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<UserResultDto> CreateAsync(UserCreationDto dto)
    {
        var existUser = await _userRepository.SelectAsync(user => user.Phone.Equals(dto.Phone));
        if (existUser is not null)
            throw new RevisionException(403, $"This user already exists with = {dto.Phone}");

        var result = PasswordHasher.Hash(dto.Password);
        var mappedUser = _mapper.Map<User>(dto);
        mappedUser.Role = Role.User;
        mappedUser.Salt = result.Salt;
        mappedUser.PasswordHash = result.Hash;
        mappedUser.CreatedAt = TimeHelper.GetDateTime();

        await _userRepository.AddAsync(mappedUser);
        await _userRepository.SaveAsync();

        return _mapper.Map<UserResultDto>(mappedUser);
    }

    public async Task<UserResultDto> UpdateAsync(long id, UserUpdateDto dto)
    {
        var existUser = await _userRepository.SelectAsync(user => user.Id.Equals(id))
            ?? throw new RevisionException(404, "This user is not found");

        var checkUser = await _userRepository.SelectAsync(user => user.Phone.Equals(dto.Phone)
        && !user.Phone.Equals(existUser.Phone));

        if (checkUser is not null)
            throw new RevisionException(403, $"This user already exists with = {dto.Phone}");

        var mappedUser = _mapper.Map(dto, existUser);

        mappedUser.Id = id;
        mappedUser.Role = Role.User;
        mappedUser.UpdatedAt = TimeHelper.GetDateTime();

        _userRepository.Update(mappedUser);
        await _userRepository.SaveAsync();

        return _mapper.Map<UserResultDto>(mappedUser);

    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existUser = await _userRepository.SelectAsync(user => user.Id.Equals(id))
           ?? throw new RevisionException(404, "This user is not found");

        _userRepository.Delete(existUser);
         await _userRepository.SaveAsync();
        return true;
    }

    public async Task<UserResultDto> GetByIdAsync(long id)
    {
        var existUser = await _userRepository.SelectAsync(user => user.Id.Equals(id))
           ?? throw new RevisionException(404, "This user is not found");

        return _mapper.Map<UserResultDto>(existUser);
    }

    public async Task<IEnumerable<UserResultDto>> GetAllAsync(PaginationParams pagination)
    {
        var users = await _userRepository.SelectAll().ToPaginate(pagination).ToListAsync();
        return _mapper.Map<IEnumerable<UserResultDto>>(users);
    }

    public async Task<UserResultDto> UpgradeRoleAsync(long id, Role role)
    {
        var existUser = await _userRepository.SelectAsync(u => u.Id.Equals(id))
             ?? throw new RevisionException(404, "This user is not found");

        existUser.Id = id;
        existUser.Role = role;
        existUser.UpdatedAt = TimeHelper.GetDateTime();

        _userRepository.Update(existUser);
        await _userRepository.SaveAsync();

        return _mapper.Map<UserResultDto>(existUser);
    }
}