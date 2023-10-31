using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Revision.DataAccess.Contexts;
using Revision.Shared.Helpers;
using Revision.WebApi.Extensions;
using Revision.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RevisionDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Services
builder.Services.AddCustomServices();

var app = builder.Build();

EnvironmentHelper.CountryPath = Path.GetFullPath(builder.Configuration.GetValue<string>(("FilePath:CountriesFilePath")));
EnvironmentHelper.RegionPath = Path.GetFullPath(builder.Configuration.GetValue<string>(("FilePath:RegionsFilePath")));
EnvironmentHelper.DistrictPath = Path.GetFullPath(builder.Configuration.GetValue<string>(("FilePath:DistrictsFilePath")));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
