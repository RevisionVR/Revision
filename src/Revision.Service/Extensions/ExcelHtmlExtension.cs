namespace Revision.Service.Extensions;

public static class ExcelHtmlExtension
{
    public static string GenerateHTMLTable<T>(IEnumerable<string> headers, IEnumerable<T> data,
        Func<T, IEnumerable<string>> dataSelector)
    {
        string table = "<!DOCTYPE html>" +
            "<html><head><meta charset='utf-8'/><title></title>" +
            "<style>table, th, td " +
            "{border: 1px solid black; border-collapse: collapse;}" +
            "</style>" +
            "</head>" +
            "<body><table style='width:100%;'>" +
            "<thead>" +
            "<tr style='font-weight:bold;'>";

        //table headers
        foreach (var header in headers)
            table += "<td>" + header + "</td>";

        table += "</tr></thead><tbody>";

        //table rows
        foreach (var item in data)
        {
            table += "<tr>";
            var rowData = dataSelector(item);
            foreach (var cell in rowData)
                table += "<td>" + cell + "</td>";

            table += "</tr>";
        }

        table += "</tbody></table></body></html>";

        return table;
    }
}