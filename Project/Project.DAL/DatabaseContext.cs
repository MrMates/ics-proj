using Microsoft.EntityFrameworkCore;
using Project.DAL.Seeds;

namespace Project.DAL;

public class DatabaseContext : DbContext
{
    private readonly bool _seedDemoData;

    public DatabaseContext(DbContextOptions contextOptions, bool seedDemoData = false)
        : base(contextOptions) =>
        _seedDemoData = seedDemoData;

    public DbSet<User> Users => Set<User>();

    public DbSet<Activity> Activities => Set<Activity>();

    public DbSet<Project> Projects => Set<Project>();

    public DbSet<UserProject> UserProjects => Set<UserProject>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(i => i.Activities)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserProject>()
            .HasKey(i => new { i.UserId, i.ProjectId });

        modelBuilder.Entity<UserProject>()
            .HasOne(i => i.User)
            .WithMany(i => i.UserProjects)
            .HasForeignKey(i => i.UserId);

        modelBuilder.Entity<UserProject>()
            .HasOne(i => i.Project)
            .WithMany(i => i.UserProjects)
            .HasForeignKey(i => i.ProjectId);

        modelBuilder.Entity<Project>()
            .HasMany(i => i.Activities)
            .WithOne()
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        if(_seedDemoData)
        {
            UserSeeds.Seed(modelBuilder);
            ActivitySeeds.Seed(modelBuilder);
            ProjectSeeds.Seed(modelBuilder);
        }







    }

}



    

