using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Project.DAL;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Activity> Activities => Set<Activity>();

    public DbSet<Project> Projects => Set<Project>();

    public DbSet<UserProject> UserProjects => Set<UserProject>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=database.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

      //  modelBuilder.Entity<User>()
        //    .HasMany(i => i.Activity);


    }

}



    

