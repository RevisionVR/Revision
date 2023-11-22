using Revision.Service.Commons.Helpers;
using Revision.Service.Commons.Models;
using System.Text;

namespace Revision.Service.Extensions;

public static class ExcelExtension
{
    public static async Task<ExcelModel> GenerateExcelFileAsync(string excelData)
    {
        byte[] excelBytes = Encoding.UTF8.GetBytes(excelData);

        string fileName = $"GeneratedReport_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

        var webrootPath = Path.Combine(PathHelper.WebRootPath, "ExcelFiles");
        if (!Directory.Exists(webrootPath))
            Directory.CreateDirectory(webrootPath);

        string filePath = Path.Combine(webrootPath, fileName);
        await File.WriteAllBytesAsync(filePath, excelBytes);

        var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        return new ExcelModel
        {
            Bytes = excelBytes,
            FileName = filePath,
            ContentType = contentType,
        };
    }
}