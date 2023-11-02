using Microsoft.AspNetCore.Mvc;
using Revision.Service.DTOs.Users;
using Revision.Service.Interfaces.Auth;
using Revision.Service.Validations.Users;

namespace Revision.WebApi.Controllers.Admin.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminAuthController : AdminBaseController
    {
        private IAuthService _authServise;

        public AdminAuthController(IAuthService authService)
        {
            this._authServise = authService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] UserCreationDto dto)
        {
            var validator = new UserCreationDtoValidator();
            var validation = await validator.ValidateAsync(dto);

            if (validation.IsValid)
                return Ok(await _authServise.RegisterAsync(dto));

            return BadRequest(validation.Errors);
        }
    }
}
