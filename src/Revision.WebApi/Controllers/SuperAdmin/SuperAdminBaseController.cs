using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Revision.WebApi.Controllers.SuperAdmin;

[Authorize(Roles = "SuperAdmin")]
public class SuperAdminBaseController : ControllerBase
{ }