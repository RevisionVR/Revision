using Revision.DataAccess.IRepositories;
using Revision.DataAccess.Repositories;
using Revision.Service.Interfaces.Addresses;
using Revision.Service.Mappers;
using Revision.Service.Services.Addresses;

namespace Revision.WebApi.Extensions;

public static class ServicesCollection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<IDistrictService, DistrictService>();
        services.AddScoped<IRegionService, RegionService>();
    }
}
