namespace Revision.Service.Commons.Models;

public class ExcelModel
{
    public byte[] Bytes { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
}