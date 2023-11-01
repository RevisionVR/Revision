﻿using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Newtonsoft.Json;
using Revision.DataAccess.IRepositories;
using Revision.DataAccess.Repositories;
using Revision.Service.Interfaces.Addresses;
using Revision.Service.Interfaces.Devices;
using Revision.Service.Interfaces.Educations;
using Revision.Service.Interfaces.Payments;
using Revision.Service.Interfaces.Subjects;
using Revision.Service.Interfaces.Topics;
using Revision.Service.Mappers;
using Revision.Service.Services.Addresses;
using Revision.Service.Services.Devices;
using Revision.Service.Services.Educations;
using Revision.Service.Services.Payments;
using Revision.Service.Services.Subjects;
using Revision.Service.Services.Topics;
using Revision.WebApi.Middlewares;

namespace Revision.WebApi.Extensions;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        //SlugParameterTransformer make upper case to lower case
        services.AddControllers(options =>
        options.Conventions.Add(new RouteTokenTransformerConvention(new SlugParameterTransformer())));


        //Json serializer
        services.AddControllers().AddNewtonsoftJson(o =>
        {
            o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });


        //Auto mapping Dependency Injection
        services.AddAutoMapper(typeof(MappingProfile));

        //Generic repository
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        //Addresses
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<IDistrictService, DistrictService>();
        services.AddScoped<IRegionService, RegionService>();

        //Subject category
        services.AddScoped<ISubjectCategoryService, SubjectCategoryService>();
        services.AddScoped<ISubjectService, SubjectService>();

        //Education category
        services.AddScoped<IEducationCategoryService, EducationCategoryService>();
        services.AddScoped<IEducationService, EducationService>();

        //Topics
        services.AddScoped<ITopicService, TopicService>();

        //Payment
        services.AddScoped<ITopicPaymentService, TopicPaymentService>();
        services.AddScoped<IDeviceService, DeviceService>();
    }
}