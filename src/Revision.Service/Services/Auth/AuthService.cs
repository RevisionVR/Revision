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
using Revision.Service.Validations.Users;

namespace Revision.Service.Services.Auth;

public class AuthService : IAuthService
{
    private IMapper _mapper;
    private ITokenService _token;
    private ISmsSender _smsSender;
    private IRepository<User> _userRepository;

    public AuthService(
        IMapper mapper,
        ISmsSender smsSender,
        ITokenService tokenService,
        IRepository<User> userRepository)
    {
        _mapper = mapper;
        _token = tokenService;
        _smsSender = smsSender;
        _userRepository = userRepository;
    }

    public async Task<(bool Result, string token)> RegisterAsync(UserCreationDto dto)
    {
        var validation = new UserCreationDtoValidator();
        var isValidUser = validation.Validate(dto);
        if (!isValidUser.IsValid)
            throw new RevisionException(400, isValidUser.Errors.FirstOrDefault().ToString());

        var existUser = await _userRepository.SelectAsync(user => user.Phone.Equals(dto.Phone));
        if (existUser is not null)
            throw new RevisionException(403, $"This user already exists this phone = {dto.Phone}");

        var result = PasswordHasher.Hash(dto.Password);
        var mappedUser = _mapper.Map<User>(dto);
        mappedUser.Role = Role.User;
        mappedUser.Salt = result.Salt;
        mappedUser.PasswordHash = result.Hash;
        mappedUser.CreatedAt = TimeHelper.GetDateTime();

        var a = await _userRepository.AddAsync(mappedUser);
        var b = await _userRepository.SaveAsync();

        SmsSenderDto smsSender = new SmsSenderDto();
        smsSender.Title = "RevisionVr";
        smsSender.Content = "Your login: " + dto.Phone + "\n" + "and password: " + dto.Password;
        //var resultSms = await _smsSender.SendAsync(smsSender);

        var token = _token.GenerateTokenAsync(mappedUser);

        return (Result: true, token:token);
    }

    public async Task<(bool Result, string token)> LoginAsync(UserLoginDto dto)
    {
        var existUser = await _userRepository.SelectAsync(user =>
        user.Phone.Equals(dto.Phone) || user.Email.Equals(dto.Email));
        if (existUser is null)
            throw new RevisionException(404, "This user is not found");

        var hasherResult = PasswordHasher.Verify(dto.password, existUser.PasswordHash, existUser.Salt);
        if (!hasherResult)
            throw new RevisionException(400, "Phone or password is invalid");

        var token = _token.GenerateTokenAsync(existUser);
        return (Result: true, token);
    }

    public Task<(bool Result, int CachedMinutes)> ResetPasswordAsync(UserResetPasswordDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<(bool Result, string Token)> VerifyResetPasswordAsync(string phone, int code)
    {
        throw new NotImplementedException();
    }
}