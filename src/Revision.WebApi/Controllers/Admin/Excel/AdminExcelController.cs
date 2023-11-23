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

    [HttpGet("get-devices-file")]
    public async Task<IActionResult> GetDevicesAsync()
    {
        var result = await _excelService.GetDevicesFileAsync();
        return File(result.Bytes, result.ContentType, result.FileName);
    }

    [HttpGet("get-topics-file")]
    public async Task<IActionResult> GetTopicsAsync()
    {
        var result = await _excelService.GetTopicsFileAsync();
        return File(result.Bytes, result.ContentType, result.FileName);
    }

    [HttpGet("get-educations-file")]
    public async Task<IActionResult> GetEducationsAsync()
    {
        var result = await _excelService.GetEducationsFileAsync();
        return File(result.Bytes, result.ContentType, result.FileName);
    }

    [HttpGet("get-topic-payments-file")]
    public async Task<IActionResult> GetTopicPaymentsAsync()
    {
        var result = await _excelService.GetTopicPaymentsFileAsync();
        return File(result.Bytes, result.ContentType, result.FileName);
    }

    [HttpGet("get-device-payments-file")]
    public async Task<IActionResult> GetDevicePaymentsAsync()
    {
        var result = await _excelService.GetDevicePaymentsFileAsync();
        return File(result.Bytes, result.ContentType, result.FileName);
    }

    [HttpGet("get-education-devices-file")]
    public async Task<IActionResult> GetEducationDevicesAsync()
    {
        var result = await _excelService.GetEducationDevicesFileAsync();
        return File(result.Bytes, result.ContentType, result.FileName);
    }

    [HttpGet("get-users-education-file")]
    public async Task<IActionResult> GetUsersEducationAsync()
    {
        var result = await _excelService.GetUsersEducationFileAsync();
        return File(result.Bytes, result.ContentType, result.FileName);
    }
}