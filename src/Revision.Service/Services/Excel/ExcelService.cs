using Microsoft.EntityFrameworkCore;
using Revision.DataAccess.IRepositories;
using Revision.Domain.Entities.Devices;
using Revision.Domain.Entities.Educations;
using Revision.Domain.Entities.Payments;
using Revision.Domain.Entities.Topics;
using Revision.Domain.Entities.Users;
using Revision.Service.Commons.Models;
using Revision.Service.Exceptions;
using Revision.Service.Extensions;
using Revision.Service.Interfaces.Devices;
using Revision.Service.Interfaces.Excel;

namespace Revision.Service.Services.Excel;

public class ExcelService : IExcelService
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Topic> _topicRepository;
    private readonly IRepository<Device> _deviceRepository;
    private readonly IDeviceCountService _deviceCountService;
    private readonly IRepository<Education> _educationRepository;
    private readonly IRepository<TopicPayment> _topicPaymentRepository;
    private readonly IRepository<DevicePayment> _devicePaymentRepository;
    private readonly IRepository<UserEducation> _userEducationRepository;
    public ExcelService(
        IRepository<User> userRepository,
        IRepository<Topic> topicRepository,
        IRepository<Device> deviceRepository,
        IDeviceCountService deviceCountService,
        IRepository<Education> educationRepository,
        IRepository<TopicPayment> topicPaymentRepository,
        IRepository<DevicePayment> devicePaymentRepository,
        IRepository<UserEducation> userEducationRepository)
    {
        _userRepository = userRepository;
        _topicRepository = topicRepository;
        _deviceRepository = deviceRepository;
        _deviceCountService = deviceCountService;
        _educationRepository = educationRepository;
        _topicPaymentRepository = topicPaymentRepository;
        _devicePaymentRepository = devicePaymentRepository;
        _userEducationRepository = userEducationRepository;
    }

    public async Task<ExcelModel> GetUsersFileAsync()
    {
        var users = await _userRepository.SelectAll().ToListAsync();
        if (!users.Any() || users is null)
            throw new RevisionException(404, "Users collection is empty");

        var headers = new string[] 
        { 
            "Last name", "First name", "Email", "Phone",
            "Gender", "Role", "CreatedAt", "UpdatedAt" 
        };

        var excelData = ExcelHtmlExtension.GenerateHTMLTable(headers, users, t =>
        {
            return new List<string>
            {
                t.LastName, t.FirstName, t.Email, t.Phone,
                t.Gender.ToString(), t.Role.ToString(),
                t.CreatedAt.ToString(), t.UpdatedAt.ToString()
            };
        });

        return await ExcelExtension.GenerateExcelFileAsync(excelData);
    }

    public async Task<ExcelModel> GetDevicesFileAsync()
    {
        var devices = await _deviceRepository.SelectAll(
            includes: new[] { "Education" }).ToListAsync();
        if (!devices.Any() || devices is null)
            throw new RevisionException(404, "Devices collection is empty");

        var headers = new string[]
        {
            "Education name","Phone", "Unique Id", "Price", "Glove", 
            "Fragrant", "Status", "CreatedAt", "UpdatedAt"
        };

        var excelData = ExcelHtmlExtension.GenerateHTMLTable(headers, devices, t =>
        {
            return new List<string>
            {
                t.Education.Name, t.Education.Phone,
                t.UniqueId, t.Price.ToString(), t.Glove.ToString(),
                t.Fragrant.ToString(), t.Status.ToString(),
                t.CreatedAt.ToString(), t.UpdatedAt.ToString()
            };
        });

        return await ExcelExtension.GenerateExcelFileAsync(excelData);
    }

    public async Task<ExcelModel> GetTopicsFileAsync()
    {
        var topics = await _topicRepository.SelectAll(
            includes: new[] { "Subject.SubjectCategory" }).ToListAsync();
        if (!topics.Any() || topics is null)
            throw new RevisionException(404, "Topics collection is empty");

        var headers = new string[]
        {
            "Subject category name", "Subject name", 
            "Topic name", "Price", "CreatedAt", "UpdatedAt"
        };

        var excelData = ExcelHtmlExtension.GenerateHTMLTable(headers, topics, t =>
        {
            return new List<string>
            {
                t.Subject.SubjectCategory.Name, 
                t.Subject.Name, t.Name, t.Price.ToString(),
                t.CreatedAt.ToString(), t.UpdatedAt.ToString(),
            };
        });

        return await ExcelExtension.GenerateExcelFileAsync(excelData);
    }

    public async Task<ExcelModel> GetEducationsFileAsync()
    {
        var educations = await _educationRepository.SelectAll( 
            includes: new[]
            {
                "Address.Country",
                 "Address.District",
                 "Address.Region",
                 "EducationCategory" 
            }).ToListAsync();

        if (!educations.Any() || educations is null)
            throw new RevisionException(404, "Educations collection is empty");

        var headers = new string[]
        {
            "Education category name", "Education name", "Phone", 
            "Email", "Country", "Region","District", "Home",
            "Description", "CreatedAt", "UpdatedAt"
        };

        var excelData = ExcelHtmlExtension.GenerateHTMLTable(headers, educations, t =>
        {
            return new List<string>
            {
                t.EducationCategory.Name, t.Name, t.Phone, t.Email,
                t.Address.Country.Name, t.Address.Region.Name, t.Address.District.Name,
                t.Address.Home,t.Description, t.CreatedAt.ToString(), t.UpdatedAt.ToString(),
            };
        });

        return await ExcelExtension.GenerateExcelFileAsync(excelData);
    }

    public async Task<ExcelModel> GetEducationDevicesFileAsync()
    {
        var devices = await _deviceCountService.GetCountAllAsync();
        if (!devices.Any() || devices is null)
            throw new RevisionException(404, "Devices collection is empty");

        var headers = new string[]
        {
            "Education name", "Phone", "Devices", 
            "Gloves","Fragrants",
            "Active", "No active"
        };

        var excelData = ExcelHtmlExtension.GenerateHTMLTable(headers, devices, t =>
        {
            return new List<string>
            {
                t.Education.Name, t.Education.Phone, t.Count.ToString(), t.Gloves.ToString(),
                t.Fragrants.ToString(), t.Active.ToString(), t.NoActive.ToString()
            };
        });

        return await ExcelExtension.GenerateExcelFileAsync(excelData);
    }

    public async Task<ExcelModel> GetTopicPaymentsFileAsync()
    {
        var topicPayments = await _topicPaymentRepository.SelectAll(
            includes: new[] { "Topic", "Education" }).ToListAsync();
        if (!topicPayments.Any() || topicPayments is null)
            throw new RevisionException(404, "Topics payment collection is empty");

        var headers = new string[]
        {
            "Education name","Phone","Topic name", "Price",
            "Last payment date", "Next payment date",
            "CreatedAt", "UpdatedAt"
        };

        var excelData = ExcelHtmlExtension.GenerateHTMLTable(headers, topicPayments, t =>
        {
            return new List<string>
            {
                t.Education.Name, t.Education.Phone, t.Topic.Name, 
                t.Price.ToString(), t.LastDate.ToString(), t.NextDate.ToString(),
                t.CreatedAt.ToString(), t.UpdatedAt.ToString(),
            };
        });

        return await ExcelExtension.GenerateExcelFileAsync(excelData);
    }

    public async Task<ExcelModel> GetDevicePaymentsFileAsync()
    {
        var devicePayments = await _devicePaymentRepository.SelectAll(
            includes: new[] {"Education" }).ToListAsync();
        if (!devicePayments.Any() || devicePayments is null)
            throw new RevisionException(404, "Topics payment collection is empty");

        var headers = new string[]
        {
            "Education name","Phone", "Price",
            "Last payment date", "Next payment date",
            "CreatedAt", "UpdatedAt"
        };

        var excelData = ExcelHtmlExtension.GenerateHTMLTable(headers, devicePayments, t =>
        {
            return new List<string>
            {
                t.Education.Name, t.Education.Phone, t.Price.ToString(),
                t.LastDate.ToString(), t.NextDate.ToString(),
                t.CreatedAt.ToString(), t.UpdatedAt.ToString(),
            };
        });

        return await ExcelExtension.GenerateExcelFileAsync(excelData);
    }

    public async Task<ExcelModel> GetUsersEducationFileAsync()
    {
        var userEducations = await _userEducationRepository.SelectAll(
            includes: new[] { "Education","User" }).ToListAsync();
        if (!userEducations.Any() || userEducations is null)
            throw new RevisionException(404, "Topics payment collection is empty");

        var headers = new string[]
        {
            "Education name","Phone", "Last name",
            "First name", "Role",
            "CreatedAt", "UpdatedAt"
        };

        var excelData = ExcelHtmlExtension.GenerateHTMLTable(headers, userEducations, t =>
        {
            return new List<string>
            {
                t.Education.Name, t.Education.Phone, t.User.LastName,
                t.User.FirstName, t.User.Role.ToString(),
                t.CreatedAt.ToString(), t.UpdatedAt.ToString(),
            };
        });

        return await ExcelExtension.GenerateExcelFileAsync(excelData);
    }
}