using Revision.Service.Commons.Models;

namespace Revision.Service.Interfaces.Excel;

public interface IExcelService
{
    Task<ExcelModel> GetUsersFileAsync();
    Task<ExcelModel> GetTopicsFileAsync();
    Task<ExcelModel> GetDevicesFileAsync();
    Task<ExcelModel> GetEducationsFileAsync();
    Task<ExcelModel> GetTopicPaymentsFileAsync();
    Task<ExcelModel> GetDevicePaymentsFileAsync();
    Task<ExcelModel> GetUsersEducationFileAsync();
    Task<ExcelModel> GetEducationDevicesFileAsync();
}