using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Users;
using Revision.Domain.Enums;
using Revision.Service.Commons.Helpers;
using Revision.Service.Commons.Security;
using Revision.Service.DTOs;
using Revision.Service.DTOs.Notifications;
using Revision.Service.DTOs.Users;
using Revision.Service.Exceptions;
using Revision.Service.Interfaces.Auth;
using Revision.Service.Interfaces.Notifications;
using Revision.Service.Validations.Users;

namespace Revision.Service.Services.Auth;

public class AuthService : IAuthService
{

    private const int CACHED_FOR_MINUTS_VEFICATION = 5;
    private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;
    private const string VERIFY_REGISTER_CACHE_KEY = "verify_reset_password_";

    private const string Reset_CACHE_KEY = "reset_";

    private IMapper _mapper;
    private ITokenService _token;
    private ISmsSender _smsSender;
    private IRepository<User> _userRepository;
    private IMemoryCache _memoryCache;


    public AuthService(
        IMapper mapper,
        ISmsSender smsSender,
        ITokenService tokenService,
        IRepository<User> userRepository,
        IMemoryCache memoryCache)
    {
        _mapper = mapper;
        _token = tokenService;
        _smsSender = smsSender;
        _userRepository = userRepository;
        _memoryCache = memoryCache;
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

        var password = PasswordGenerate.Password();
        var result = PasswordHasher.Hash(password);
        var mappedUser = _mapper.Map<User>(dto);
        mappedUser.Role = Role.User;
        mappedUser.Salt = result.Salt;
        mappedUser.PasswordHash = result.Hash;
        mappedUser.CreatedAt = TimeHelper.GetDateTime();

        var dbResult = await _userRepository.AddAsync(mappedUser);
        var resultDb = await _userRepository.SaveAsync();

        SmsSenderDto smsSender = new SmsSenderDto();
        smsSender.Title = "RevisionVr\n";
        smsSender.Content = "login: " + dto.Phone + "\npassword: " + password;
        smsSender.Recipient = dto.Phone.Substring(1);
        var resultSms = await _smsSender.SendAsync(smsSender);

        if (resultSms != true)
            return (false, "");

        var token = _token.GenerateTokenAsync(dbResult);

        return (Result: resultDb, token: token);
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

    public async Task<(bool Result, int CachedMinutes)> ResetPasswordAsync(UserResetPasswordDto dto)
    {
        var existUser = await _userRepository.SelectAsync(user =>
        user.Phone.Equals(dto.Phone) || user.Email.Equals(dto.Email));

        if (existUser is null)
            throw new RevisionException(404, "This user is not found");

        var userCreateDto = _mapper.Map<User>(existUser);
        var resultPassword = PasswordHasher.Hash(dto.NewPassword);
        userCreateDto.PasswordHash = resultPassword.Hash;
        userCreateDto.Salt = resultPassword.Salt;

        if (_memoryCache.TryGetValue(Reset_CACHE_KEY + dto.Phone, out User userCreationDto))
        {
            _memoryCache.Remove(dto.Phone);
        }
        else
        {
            _memoryCache.Set(Reset_CACHE_KEY + dto.Phone, userCreateDto,
                TimeSpan.FromMinutes(CACHED_FOR_MINUTS_VEFICATION));
        }

        if (_memoryCache.TryGetValue(Reset_CACHE_KEY + dto.Phone, out User userDto))
        {
            VerificationDto verificationDto = new VerificationDto();
            verificationDto.Attempt = 0;
            verificationDto.CreatedAt = TimeHelper.GetDateTime();
            verificationDto.Code = CodeGenerator.CodeGeneratorPhoneNumber();
            _memoryCache.Set(dto.Phone, verificationDto, TimeSpan.FromMinutes(CACHED_FOR_MINUTS_VEFICATION));

            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + dto.Phone, out VerificationDto oldVerificationDto))
            {
                _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + dto.Phone);
            }

            _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + dto.Phone, verificationDto,
            TimeSpan.FromMinutes(VERIFICATION_MAXIMUM_ATTEMPTS));

            SmsSenderDto smsSender = new SmsSenderDto();
            smsSender.Title = "RevisionVr\n";
            smsSender.Content = "Your verification code : " + verificationDto.Code;
            smsSender.Recipient = dto.Phone.Substring(1);
            var resultSms = true;// await _smsSender.SendAsync(smsSender);

            if (resultSms is true)
                return (Result: true, CachedVerificationMinutes: CACHED_FOR_MINUTS_VEFICATION);
            else
                return (Result: false, CACHED_FOR_MINUTS_VEFICATION: 0);
        }
        else
        {
            throw new RevisionException(400, "Expired time");
        }
    }

    public async Task<(bool Result, string Token)> VerifyResetPasswordAsync(string phone, int code)
    {
        if (_memoryCache.TryGetValue(Reset_CACHE_KEY + phone, out User userRegisterDto))
        {
            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + phone, out VerificationDto verificationDto))
            {
                if (verificationDto.Attempt >= VERIFICATION_MAXIMUM_ATTEMPTS)
                    throw new RevisionException(500, "Verification Too Many Requests");
                else if (verificationDto.Code == code)
                {
                    var user = _mapper.Map<User>(userRegisterDto);
                    user.UpdatedAt = TimeHelper.GetDateTime();

                    var dResult = _userRepository.Update(user);
                    var result = await _userRepository.SaveAsync();

                    var token = _token.GenerateTokenAsync(dResult);

                    return (Result: result, Token: token);
                }
                else
                {
                    _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + phone);
                    verificationDto.Attempt++;
                    _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phone, verificationDto,
                        TimeSpan.FromMinutes(CACHED_FOR_MINUTS_VEFICATION));

                    return (Result: false, Token: "");
                }
            }
            else throw new RevisionException(400, "Expired time");
        }
        else throw new RevisionException(400, "Expired time");
    }
}