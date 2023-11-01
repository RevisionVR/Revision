using Microsoft.AspNetCore.Mvc;

namespace Revision.WebApi.Controllers.Common;

[ApiController]
[Route("[controller]")]
//[Authorize(Roles = "User")]
public class BaseController : ControllerBase
{ }