using Microsoft.Extensions.Logging;
using Crow.Interfaces;
using Crow.Repositories;
using Crow;
using Crow.MVVM.Models.Services; // Add this using directive

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        // Register dbPath as a service
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "app_database.db");
        builder.Services.AddSingleton(new DatabaseService(dbPath));

        // Register the string type with a specific value
        builder.Services.AddSingleton<string>(dbPath);

        // Register App
        builder.Services.AddSingleton<App>();

        builder.Services.AddSingleton<IUserRepository>(provider =>
        {
            return new UserRepository(dbPath);
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
