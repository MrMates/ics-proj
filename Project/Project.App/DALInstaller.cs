using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;
using Project.DAL;
using Project.DAL.Factories;
using Project.DAL.Mappers;

namespace Project.App
{
    public static class DALInstaller
    {
        public static IServiceCollection AddDALServices(this IServiceCollection services)
        {
            // TODO: Turn off seeding and recreating for final product
            DALOptions dalOptions = new()
            {
                Sqlite = new()
                {
                    DatabaseName = "ics_project.db",
                    Enabled = true,
                    RecreateDatabaseEachTime = false,
                    SeedDemoData = false
                }
            };

            services.AddSingleton<DALOptions>(dalOptions);

            string databaseFilePath = Path.Combine(FileSystem.AppDataDirectory, dalOptions.Sqlite.DatabaseName);
            services.AddSingleton<IDbContextFactory<DatabaseContext>>(provider => new DbContextSqlLiteFactory(databaseFilePath, dalOptions.Sqlite.SeedDemoData));
            services.AddSingleton<IDbMigrator, SqliteDbMigrator>();

            services.AddSingleton<ActivityEntityMapper>();
            services.AddSingleton<ProjectEntityMapper>();
            services.AddSingleton<UserEntityMapper>();
            services.AddSingleton<UserProjectEntityMapper>();

            return services;
        }
    }
}
