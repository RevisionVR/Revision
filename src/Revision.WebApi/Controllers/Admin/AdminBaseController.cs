using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Revision.WebApi.Controllers.Admin;

[Authorize(Roles = "Admin, SuperAdmin")]
public class AdminBaseController : ControllerBase
{ }