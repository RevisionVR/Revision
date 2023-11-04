using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Revision.WebApi.Controllers.Common;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Roles = "User")]
public class BaseController : ControllerBase
{ }