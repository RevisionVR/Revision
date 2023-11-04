using Microsoft.AspNetCore.Http;
using Revision.Service.Interfaces.Auth;
using System.Security.Claims;

namespace Revision.Service.Services.Auth;

public class IdentityService : IIdentityService
{
    private IHttpContextAccessor _accessor;
    public IdentityService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public long Id
    {
        get
        {
            if (_accessor.HttpContext is null)
                return 0;

            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(op => op.Type == "Id");

            if (claim is null)
                return 0;
            else
                return long.Parse(claim.Value);
        }
    }

    public string RoleName
    {
        get
        {
            if (_accessor.HttpContext is null)
                return string.Empty;

            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(op => op.Type == ClaimTypes.Role);
            if (claim is null)
                return string.Empty;
            else
                return claim.Value;
        }
    }

    public string FirstName
    {
        get
        {
            if (_accessor.HttpContext is null)
                return string.Empty;

            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(op => op.Type == "FirstName");

            if (claim is null)
                return string.Empty;
            else
                return claim.Value;
        }
    }

    public string LastName
    {
        get
        {
            if (_accessor.HttpContext is null)
                return string.Empty;

            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(op => op.Type == "LastName");

            if (claim is null)
                return string.Empty;
            else
                return claim.Value;
        }
    }

    public string Phone
    {
        get
        {
            if (_accessor.HttpContext is null)
                return string.Empty;

            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(op => op.Type == "Phone");

            if (claim is null)
                return string.Empty;
            else
                return claim.Value;
        }
    }

    public string Email
    {
        get
        {
            if (_accessor.HttpContext is null)
                return string.Empty;

            var claim = _accessor.HttpContext.User.Claims.FirstOrDefault(op => op.Type == "Email");

            if (claim is null)
                return string.Empty;
            else
                return claim.Value;
        }
    }
}
