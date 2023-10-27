using Microsoft.EntityFrameworkCore;
using Revision.Domain.Entities.Users;
using Revision.Domain.Entities.Topics;
using Revision.Domain.Entities.Devices;
using Revision.Domain.Entities.Subjects;
using Revision.Domain.Entities.Payments;
using Revision.Domain.Entities.Addresses;
using Revision.Domain.Entities.Educations;
using Revision.Domain.Entities.Chats.Rooms;
using Revision.Domain.Entities.Chats.Messages;
using Revision.Domain.Entities.Chats.Conversations;
using Revision.Domain.Entities.Categories.SubjectCategories;
using Revision.Domain.Entities.Categories.EducationCategories;

namespace Revision.DataAccess.Contexts;

public class RevisionDbContext : DbContext
{
    public RevisionDbContext(DbContextOptions<RevisionDbContext> options) : base(options)
    { }

    public DbSet<User> Users { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<AdminRoom> AdminRooms { get; set; }
    public DbSet<EducationRoom> EducationRooms { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<EducationCategory> EducationCategories { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<DevicePayment> DevicePayments { get; set; }
    public DbSet<TopicPayment> TopicPayments { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<SubjectCategory> SubjectCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Filtering "IsDeleted" status for entities
        modelBuilder.Entity<User>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Region>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<District>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Country>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Address>().HasQueryFilter(e => !e.IsDeleted);

        modelBuilder.Entity<Message>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Room>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Conversation>().HasQueryFilter(e => !e.IsDeleted);

        modelBuilder.Entity<AdminRoom>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<EducationRoom>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Education>().HasQueryFilter(e => !e.IsDeleted);

        modelBuilder.Entity<EducationCategory>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Device>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<DevicePayment>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<TopicPayment>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Topic>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Subject>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<SubjectCategory>().HasQueryFilter(e => !e.IsDeleted);
        #endregion
    }
}