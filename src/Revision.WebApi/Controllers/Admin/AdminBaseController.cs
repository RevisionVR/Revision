using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Revision.WebApi.Controllers.Admin;

[Authorize(Roles = "Admin, SuperAdmin")]
public class AdminBaseController : ControllerBase
{ }