using Revision.Service.Commons.Models;

namespace Revision.Service.Interfaces.Excel;

public interface IExcelService
{
    Task<ExcelModel> GetUsersFileAsync();
}