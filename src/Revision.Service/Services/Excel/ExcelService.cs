using Microsoft.EntityFrameworkCore;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Users;
using Revision.Service.Commons.Models;
using Revision.Service.Exceptions;
using Revision.Service.Extensions;
using Revision.Service.Interfaces.Excel;

namespace Revision.Service.Services.Excel;

public class ExcelService : IExcelService
{
    private readonly IRepository<User> _userRepository;
    public ExcelService(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ExcelModel> GetUsersFileAsync()
    {
        var users = await _userRepository.SelectAll().ToListAsync();
        if (!users.Any())
            throw new RevisionException(404, "Users table is empty");

        var headers = new string[] 
        { 
            "Last name", "First name", "Email", "Phone", "Gender", "Role", "CreatedAt", "UpdatedAt" 
        };

        var excelData = ExcelHtmlExtension.GenerateHTMLTable(headers, users, u =>
        {
            return new List<string>
            {
                u.LastName, u.FirstName, u.Email, u.Phone,
                u.Gender.ToString(), u.Role.ToString(),
                u.CreatedAt.ToString(), u.UpdatedAt.ToString()
            };
        });

        return await ExcelExtension.GenerateExcelFileAsync(excelData);
    }
}