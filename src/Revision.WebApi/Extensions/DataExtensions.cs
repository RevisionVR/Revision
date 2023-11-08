using Microsoft.EntityFrameworkCore;
using Revision.DataAccess.Contexts;

namespace Revision.WebApi.Extensions;

public static class DataExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<RevisionDbContext>();
            db.Database.Migrate();
        }
    }
}