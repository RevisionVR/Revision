using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Users;
using Revision.Domain.Enums;
using Revision.Service.Commons.Helpers;
using Revision.Service.Commons.Security;
using Revision.Service.DTOs.Auth;
using Revision.Service.DTOs.Notifications;
using Revision.Service.DTOs.ResetVerification;
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
    private const string Reset_CACHE_KEY = "reset_";
    private const string VERIFY_REGISTER_CACHE_KEY = "verify_reset_password_";

    private IMapper _mapper;
    private ITokenService _token;
    private ISmsSender _smsSender;
    private IMemoryCache _memoryCache;
    private IIdentityService _identityservice;
    private IRepository<User> _userRepository;


    public AuthService(
        IMapper mapper,
        ISmsSender smsSender,
        IMemoryCache memoryCache,
        ITokenService tokenService,
        IRepository<User> userRepository,
        IIdentityService identityService)
    {
        _mapper = mapper;
        _token = tokenService;
        _smsSender = smsSender;
        _memoryCache = memoryCache;
        _userRepository = userRepository;
        _identityservice = identityService;
    }

    public async Task<AuthResult> RegisterAsync(UserCreationDto dto)
    {
        var validation = new UserCreationDtoValidator();
        var isValidUser = validation.Validate(dto);
        if (!isValidUser.IsValid)
            throw new RevisionException(400, isValidUser.Errors.FirstOrDefault().ToString());

        var existUser = await _userRepository.SelectAsync(user => user.Phone.Equals(dto.Phone));

        if (existUser is not null)
            throw new RevisionException(403, $"This user already exists this phone = {dto.Phone}");

        if (_identityservice.RoleName.ToString() == Role.Admin.ToString())
        {
            if ((dto.Role == Role.SuperAdmin || dto.Role == Role.Admin))
                throw new RevisionException(403, $"You are not create {dto.Role}");
        }

        var password = PasswordGenerate.Password();
        var result = PasswordHasher.Hash(password);
        var mappedUser = _mapper.Map<User>(dto);
        mappedUser.Salt = result.Salt;
        mappedUser.PasswordHash = result.Hash;
        mappedUser.CreatedAt = TimeHelper.GetDateTime();
        mappedUser.UpdatedAt = TimeHelper.GetDateTime();

        var user = await _userRepository.AddAsync(mappedUser);
        var resultDb = await _userRepository.SaveAsync();

        SmsSenderDto smsSender = new SmsSenderDto();
        smsSender.Title = "RevisionVr\n";
        smsSender.Content = "login: " + dto.Phone + "\npassword: " + password;
        smsSender.Recipient = dto.Phone.Substring(1);
        Console.WriteLine(dto.Phone + " # " + password);
        var resultSms = true;// await _smsSender.SendAsync(smsSender);

        AuthResult authResult = new AuthResult()
        {
            Result = resultSms,
            Token = string.Empty
        };

        if (resultSms != true)
            return authResult;

        var token = await _token.GenerateTokenAsync(user);
        authResult.Token = token;

        return authResult;
    }

    public async Task<AuthResult> LoginAsync(UserLoginDto dto)
    {
        var existUser = await _userRepository.SelectAsync(user => user.Phone.Equals(dto.Phone))
            ?? throw new RevisionException(404, "This user is not found");

        var hasherResult = PasswordHasher.Verify(dto.Password, existUser.PasswordHash, existUser.Salt);
        if (!hasherResult)
            throw new RevisionException(400, "Password is invalid");

        var token = await _token.GenerateTokenAsync(existUser);
        AuthResult authResult = new AuthResult()
        {
            Result = true,
            Token = token
        };

        return authResult;
    }

    public async Task<AuthResetPassword> ResetPasswordAsync(UserResetPasswordDto dto)
    {
        var existUser = await _userRepository.SelectAsync(user => user.Phone.Equals(dto.Phone))
            ?? throw new RevisionException(404, "This user is not found");

        var mappedUser = _mapper.Map<User>(existUser);
        var resultPassword = PasswordHasher.Hash(dto.NewPassword);
        mappedUser.PasswordHash = resultPassword.Hash;
        mappedUser.Salt = resultPassword.Salt;

        if (_memoryCache.TryGetValue(Reset_CACHE_KEY + dto.Phone, out User user))
        {
            _memoryCache.Remove(dto.Phone);
        }

        _memoryCache.Set(Reset_CACHE_KEY + dto.Phone, mappedUser,
                TimeSpan.FromMinutes(CACHED_FOR_MINUTS_VEFICATION));

        if (_memoryCache.TryGetValue(Reset_CACHE_KEY + dto.Phone, out User userEntity))
        {
            VerificationDto verificationDto = new VerificationDto();
            verificationDto.Attempt = 0;
            verificationDto.CreatedAt = TimeHelper.GetDateTime();
            verificationDto.Code = 12345; // CodeGenerator.RandomCodeGenerator();
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

            AuthResetPassword authResetPassword = new AuthResetPassword()
            {
                Result = resultSms,
                CachedMinutes = CACHED_FOR_MINUTS_VEFICATION
            };

            if (resultSms is true)
                return authResetPassword;
            else
            {
                authResetPassword.CachedMinutes = 0;
                return authResetPassword;
            }
        }
        else
        {
            throw new RevisionException(400, "Expired time");
        }
    }

    public async Task<AuthResult> VerifyResetPasswordAsync(string phone, int code)
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

                    var token = await _token.GenerateTokenAsync(dResult);

                    AuthResult authResult = new AuthResult()
                    {
                        Result = true,
                        Token = token
                    };

                    return authResult;
                }
                else
                {
                    _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + phone);
                    verificationDto.Attempt++;
                    _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phone, verificationDto,
                        TimeSpan.FromMinutes(CACHED_FOR_MINUTS_VEFICATION));

                    AuthResult authResult = new AuthResult()
                    {
                        Result = false,
                        Token = string.Empty
                    };

                    return authResult;
                }
            }

            else throw new RevisionException(400, "Expired time");
        }
        else throw new RevisionException(400, "Expired time");
    }
}