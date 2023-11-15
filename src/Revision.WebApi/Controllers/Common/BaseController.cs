using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Revision.WebApi.Controllers.Common;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "User, Admin, SuperAdmin")]
public class BaseController : ControllerBase
{ }