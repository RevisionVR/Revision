using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Revision.WebApi.Controllers.Common;

[ApiController]
[Route("[controller]")]
//[Authorize(Roles = "User")]
public class BaseController : ControllerBase
{ }