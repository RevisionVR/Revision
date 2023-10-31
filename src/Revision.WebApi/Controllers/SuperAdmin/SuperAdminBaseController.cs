using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Revision.WebApi.Controllers.SuperAdmin
{
    [Authorize(Roles ="SuperAdmin")]
    public class SuperAdminBaseController : ControllerBase
    {}
}
