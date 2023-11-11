using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Revision.WebApi.Controllers.Common;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{ }