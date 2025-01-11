using Microsoft.Extensions.Logging;
using Crow.Interfaces;
using Crow.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Crow
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder.Services.AddSingleton<IUserRepository>(provider =>
            {
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "app_database.db");
                return new UserRepository(databasePath);
            });

            builder.Services.AddSingleton<App>((services) =>
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "Themes.db");
                return new App(services, dbPath);
            });

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

            return builder.Build();
        }
    }
}
