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
    public async Task<IActionResult> PostAsync(DeviceCreationDto dto)
    {
        var validation = new DeviceCreationDtoValidator();
        var result = validation.Validate(dto);
        if (!result.IsValid)
            Ok(new Response
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
    public async Task<IActionResult> PutAsync(long id, DeviceUpdateDto dto)
    {
        var validation = new DeviceUpdateDtoValidator();
        var result = validation.Validate(dto);
        if (!result.IsValid)
            Ok(new Response
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


    [HttpPut("update/{uniqueId:string}")]
    public async Task<IActionResult> UpdateIsActiveAsync(string uniqueId, DeviceStatus status)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _deviceService.UpdateIsActiveAsync(uniqueId, status)
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


    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams pagination)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _deviceService.GetAllAsync(pagination)
        });
}