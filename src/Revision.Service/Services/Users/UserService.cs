using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Configurations;
using Revision.Domain.Entities.Users;
using Revision.Domain.Enums;
using Revision.Service.Commons.Helpers;
using Revision.Service.Commons.Security;
using Revision.Service.DTOs.Users;
using Revision.Service.Exceptions;
using Revision.Service.Extensions;
using Revision.Service.Interfaces.Users;

namespace Revision.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _userRepository;
    public UserService(IMapper mapper, IRepository<User> userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<UserResultDto> CreateAsync(UserCreationDto dto)
    {
        var existUser = await _userRepository.SelectAsync(user => user.Phone.Equals(dto.Phone));
        if (existUser is not null)
            throw new RevisionException(403, $"This user already exists with = {dto.Phone}");
        var password = PasswordGenerate.Password();
        var result = PasswordHasher.Hash(password);
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

        var checkUser = await _userRepository.SelectAsync(user => user.Phone.Equals(dto.Phone) &&
        !user.Phone.Equals(existUser.Phone));

        if (checkUser is not null)
            throw new RevisionException(403, $"This user already exists with = {dto.Phone}");
        var mappedUser = _mapper.Map(dto, existUser);

        mappedUser.Id = id;
        mappedUser.Role = existUser.Role;
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
        var users = await _userRepository.SelectAll()
            .ToPaginate(pagination)
            .ToListAsync();

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

    public async Task<UserResultDto> GetByRoleAsync(Role role)
    {
        var dbResult = _userRepository.SelectAll(user => user.Role.Equals(role)).ToList();

        if (dbResult == null)
            throw new RevisionException(404, "This users role are not found");

        return _mapper.Map<UserResultDto>(dbResult);
    }

    public async Task<UserResultDto> UpdateSecurityAsync(long id, UserSecurityUpdateDto security)
    {
        var existUser = await _userRepository.SelectAsync(user => user.Id.Equals(id));

        if (existUser == null)
            throw new RevisionException(404, "This is User Not Found");

        if (security.NewPassword == security.ReturnNewPassword)
        {
            var passwords = PasswordHasher.Hash(security.NewPassword);
            existUser.PasswordHash = passwords.Hash;
            existUser.Salt = passwords.Salt;
            existUser.UpdatedAt = TimeHelper.GetDateTime();
        }
        else
            throw new RevisionException(400, "New password not equal returnPassword");

        _userRepository.Update(existUser);
        await _userRepository.SaveAsync();

        return _mapper.Map<UserResultDto>(existUser);
    }
}