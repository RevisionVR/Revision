using AutoMapper;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Users;
using Revision.Domain.Enums;
using Revision.Service.Commons.Helpers;
using Revision.Service.Commons.Security;
using Revision.Service.DTOs.Notifications;
using Revision.Service.DTOs.Users;
using Revision.Service.Exceptions;
using Revision.Service.Interfaces.Auth;

namespace Revision.Service.Services.Auth;

public class AuthService : IAuthService
{
    private IMapper _mapper;
    private IRepository<User> _userRepository;
    public AuthService(IRepository<User> repository, IMapper mapper)
    {
        this._mapper = mapper;
        this._userRepository = repository;
    }

    public async Task<bool> RegisterAsync(UserCreationDto dto)
    {
        var existUser = await _userRepository.SelectAsync(user => user.Phone.Equals(dto.Phone));
        if (existUser is not null)
            throw new RevisionException(403, $"This user already exists this phone = {dto.Phone}");

        var result = PasswordHasher.Hash(dto.Password);
        var mappedUser = _mapper.Map<User>(dto);
        mappedUser.Role = Role.User;
        mappedUser.Salt = result.Salt;
        mappedUser.PasswordHash = result.Hash;
        mappedUser.CreatedAt = TimeHelper.GetDateTime();

        await _userRepository.AddAsync(mappedUser);
        await _userRepository.SaveAsync();

        SmsSender smsSender = new SmsSender();
        smsSender.Title = "RevisionVr";
        smsSender.Content = "Your login: " + dto.Phone + "\n" + "and password: " + dto.Password;
        // sender


        return true;
    }

    public Task<(bool Result, string token)> LoginAsync(UserLoginDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<(bool Result, int CachedMinutes)> ResetPasswordAsync(UserResetPasswordDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<(bool Result, string Token)> VerifyResetPasswordAsync(string phoneNumber, int code)
    {
        throw new NotImplementedException();
    }
}