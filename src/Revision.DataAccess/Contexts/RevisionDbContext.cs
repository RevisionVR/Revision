using Microsoft.EntityFrameworkCore;

namespace Revision.DataAccess.Contexts;

public class RevisionDbContext : DbContext
{
    public RevisionDbContext(DbContextOptions<RevisionDbContext> options) : base(options)
    { }
}