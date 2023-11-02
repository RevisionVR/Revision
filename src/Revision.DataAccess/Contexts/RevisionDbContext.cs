using Microsoft.EntityFrameworkCore;
using Revision.Domain.Entities.Addresses;
using Revision.Domain.Entities.Chats;
using Revision.Domain.Entities.Devices;
using Revision.Domain.Entities.Educations;
using Revision.Domain.Entities.Payments;
using Revision.Domain.Entities.Subjects;
using Revision.Domain.Entities.Topics;
using Revision.Domain.Entities.Users;

namespace Revision.DataAccess.Contexts;

public class RevisionDbContext : DbContext
{
    public RevisionDbContext(DbContextOptions<RevisionDbContext> options) : base(options)
    { }

    public DbSet<User> Users { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<ChatRoom> ChatRooms { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<TopicPayment> TopicPayments { get; set; }
    public DbSet<UserEducation> UserEducations { get; set; }
    public DbSet<DevicePayment> DevicePayments { get; set; }
    public DbSet<SubjectCategory> SubjectCategories { get; set; }
    public DbSet<EducationCategory> EducationCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Filtering "IsDeleted" status for entities
        modelBuilder.Entity<Chat>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Device>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<User>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Region>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<District>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Country>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Address>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Topic>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Subject>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<ChatRoom>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Education>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<TopicPayment>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<DevicePayment>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<UserEducation>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<SubjectCategory>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<EducationCategory>().HasQueryFilter(e => !e.IsDeleted);
        #endregion
    }
}