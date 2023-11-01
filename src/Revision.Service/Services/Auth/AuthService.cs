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
using Revision.Service.Interfaces.Notifications;

namespace Revision.Service.Services.Auth;

public class AuthService : IAuthService
{
    private IMapper _mapper;
    private IRepository<User> _userRepository;
    private ISmsSender _smsSender;
    private ITokenService _token;

    public AuthService(
        IRepository<User> repository,
        IMapper mapper,
        ISmsSender smsSender,
        ITokenService tokenService)
    {
        this._mapper = mapper;
        this._userRepository = repository;
        this._smsSender = smsSender;
        this._token = tokenService;
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

        SmsSenderDto smsSender = new SmsSenderDto();
        smsSender.Title = "RevisionVr";
        smsSender.Content = "Your login: " + dto.Phone + "\n" + "and password: " + dto.Password;
        var resultSms = await _smsSender.SendAsync(smsSender);

        return true;
    }

    public async Task<(bool Result, string token)> LoginAsync(UserLoginDto dto)
    {
        var dbResult = await _userRepository.SelectAsync(user => user.Phone.Equals(dto.Phone) 
            || user.Email.Equals(dto.Email));

        if (dbResult is null)
            throw new RevisionException(404, "User NotFound");

        var hasherResult = PasswordHasher.Verify(dto.password, dbResult.PasswordHash, dbResult.Salt);

        if (hasherResult == false)
            throw new RevisionException(403, "Password Is wrong ");

        var token = _token.GenerateTokenAsync(dbResult);

        return (Result: true, token);
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