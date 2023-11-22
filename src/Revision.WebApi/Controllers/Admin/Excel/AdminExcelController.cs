using Microsoft.AspNetCore.Mvc;
using Revision.Service.Interfaces.Excel;

namespace Revision.WebApi.Controllers.Admin.Excel;

public class AdminExcelController : AdminBaseController
{
    private readonly IExcelService _excelService;
    public AdminExcelController(IExcelService excelService)
    {
        _excelService = excelService;
    }

    [HttpGet("get-users-file")]
    public async Task<IActionResult> GetUsersAsync()
    {
        var result = await _excelService.GetUsersFileAsync();
        return File(result.Bytes, result.ContentType, result.FileName);
    }
}