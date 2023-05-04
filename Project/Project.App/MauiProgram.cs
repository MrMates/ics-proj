using Microsoft.Extensions.Logging;
using Project.App.Services;
using Project.BL;

namespace Project.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif


            builder.Services
                .AddDALServices()
                .AddAppServices()
                .AddBLServices();

            var app = builder.Build();

            app.Services.GetRequiredService<IDbMigrator>().Migrate();
            RegisterRouting(app.Services.GetRequiredService<INavigationService>());

            return app;
        }

        private static void RegisterRouting(INavigationService navigationService)
        {
            foreach (var route in navigationService.Routes)
            {
                Routing.RegisterRoute(route.Route, route.ViewType);
            }
        }
    }
}