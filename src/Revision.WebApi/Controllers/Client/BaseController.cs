using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Revision.WebApi.Controllers.Client
{
    [Authorize(Roles = "User")]
    public class BaseController : ControllerBase
    {}
}
