using Microsoft.AspNetCore.Mvc;
using Revision.Domain.Configurations;
using Revision.Domain.Enums;
using Revision.Service.DTOs.Devices;
using Revision.Service.Interfaces.Devices;
using Revision.Service.Validations.Devices;
using Revision.WebApi.Models;

namespace Revision.WebApi.Controllers.Admin.Devices;

public class AdminDevicesController : AdminBaseController
{
    private readonly IDeviceService _deviceService;
    public AdminDevicesController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync([FromForm] DeviceCreationDto dto)
    {
        var validation = new DeviceCreationDtoValidator();
        var result = validation.Validate(dto);
        if (result.IsValid)
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _deviceService.CreateAsync(dto)
            });

        return BadRequest(new Response
        {
            StatusCode = 400,
            Message = result.Errors.FirstOrDefault().ToString()
        });
    }


    [HttpPut("update/{id:long}")]
    public async Task<IActionResult> PutAsync(long id, [FromForm] DeviceUpdateDto dto)
    {
        var validation = new DeviceUpdateDtoValidator();
        var result = validation.Validate(dto);
        if (result.IsValid)
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Success",
                Data = await _deviceService.UpdateAsync(id, dto)
            });

        return BadRequest(new Response
        {
            StatusCode = 400,
            Message = result.Errors.FirstOrDefault().ToString()
        });
    }


    [HttpPut("update/status/{uniqueId}")]
    public async Task<IActionResult> UpdateIsActiveAsync(string uniqueId, DeviceStatus status)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _deviceService.UpdateIsActiveAsync(uniqueId.ToString(), status)
        });


    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _deviceService.DeleteAsync(id)
        });


    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _deviceService.GetByIdAsync(id)
        });


    [HttpGet("get/{uniqueId}")]
    public async Task<IActionResult> GetAsync(string uniqueId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _deviceService.GetByUniqueIdAsync(uniqueId)
        });


    [HttpGet("get-all-by-page")]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] PaginationParams pagination,
        [FromQuery] string search)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _deviceService.GetAllAsync(pagination, search)
        });


    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
    => Ok(new Response
    {
        StatusCode = 200,
        Message = "Success",
        Data = await _deviceService.GetAllAsync()
    });
}